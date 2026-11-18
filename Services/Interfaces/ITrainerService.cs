using FitTrack.DTOs.Common;
using FitTrack.DTOs.Trainer;

namespace FitTrack.Services.Interfaces;

public interface ITrainerService
{
    Task<ClientDto> AddClientAsync(int trainerId, AddClientRequest request);
    Task<PagedResult<ClientDto>> GetClientsAsync(int trainerId, PagingQuery paging);
    Task<bool> IsMyClientAsync(int trainerId, int clientId);
}
