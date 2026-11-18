using AutoMapper;
using FitTrack.DTOs.Trainer;
using FitTrack.Entities;

namespace FitTrack.Mapping;

public class TrainerProfile : Profile
{
    public TrainerProfile()
    {
        CreateMap<TrainerClient, ClientDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.Client.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Client.Email))
            .ForMember(d => d.LastWorkoutDate,
                o => o.MapFrom(s => s.Client.Workouts.Max(w => (DateTime?)w.Date)));
    }
}
