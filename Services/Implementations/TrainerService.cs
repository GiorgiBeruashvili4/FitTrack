using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitTrack.Data;
using FitTrack.DTOs.Common;
using FitTrack.DTOs.Trainer;
using FitTrack.Entities;
using FitTrack.Enums;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class TrainerService : ITrainerService
{
    private readonly Database _db;
    private readonly IMapper _mapper;

    public TrainerService(Database db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ClientDto> AddClientAsync(int trainerId, AddClientRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var client = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (client == null)
        {
            throw new NotFoundException("User with this email was not found.");
        }

        if (client.Role != Role.Member)
        {
            throw new BusinessRuleException("Only members can be added as clients.");
        }

        var alreadyClient = await _db.TrainerClients
            .AnyAsync(tc => tc.TrainerId == trainerId && tc.ClientId == client.Id);

        if (alreadyClient)
        {
            throw new ConflictException("This user is already your client.");
        }

        _db.TrainerClients.Add(new TrainerClient
        {
            TrainerId = trainerId,
            ClientId = client.Id,
            Since = DateOnly.FromDateTime(DateTime.UtcNow)
        });

        await _db.SaveChangesAsync();

        var result = await _db.TrainerClients
            .AsNoTracking()
            .Include(tc => tc.Client)
                .ThenInclude(c => c.Workouts)
            .FirstAsync(tc => tc.TrainerId == trainerId && tc.ClientId == client.Id);

        return _mapper.Map<ClientDto>(result);
    }

    public async Task<PagedResult<ClientDto>> GetClientsAsync(int trainerId, PagingQuery paging)
    {
        var query = _db.TrainerClients
        .AsNoTracking()
        .Include(tc => tc.Client)
            .ThenInclude(c => c.Workouts)
        .Where(tc => tc.TrainerId == trainerId)
        .OrderBy(tc => tc.Client.FullName);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((paging.Page - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .ToListAsync();

        return new PagedResult<ClientDto>
        {
            Items = _mapper.Map<List<ClientDto>>(items),
            TotalCount = totalCount,
            Page = paging.Page,
            PageSize = paging.PageSize
        };
    }

    public async Task<bool> IsMyClientAsync(int trainerId, int clientId)
    {
        return await _db.TrainerClients.AnyAsync(tc => tc.TrainerId == trainerId && tc.ClientId == clientId);
    }
}
