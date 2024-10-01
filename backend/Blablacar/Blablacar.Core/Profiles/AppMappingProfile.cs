using AutoMapper;
using Blablacar.Contracts.Requests.Trips.GetTrip;
using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Contracts.Requests.Trips.GetTripsUser;
using Blablacar.Contracts.Requests.Users.GetUserInfo;
using Blablacar.Core.Entities;

namespace Blablacar.Core.Profiles;

/// <summary>
/// Для маппинга
/// </summary>
public class AppMappingProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AppMappingProfile()
    {
        // GetTrips
        CreateMap<Trip, GetTripsResponseItem>()
            .ForMember(dest => dest.DriverName,
                opt => opt.MapFrom(src => src.Driver.FirstName))
            .ForMember(dest => dest.DriverAvatar,
                opt => opt.MapFrom(src => src.Driver.Avatar));
        
        // GetTrip
        CreateMap<Trip, GetTripResponse>()
            .ForMember(dest => dest.Driver,
                opt => opt.MapFrom(src => src.Driver))
            .ForMember(dest => dest.Passengers,
                opt => opt.MapFrom(src => src.Passengers));
        
        CreateMap<User, Driver>()
            .ForMember(dest => dest.DriverName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.DriverAvatar,
                opt => opt.MapFrom(src => src.Avatar));

        CreateMap<User, Passenger>();
        
        // GetUserInfo
        CreateMap<User, GetUserInformationResponse>()
            .ForMember(dest => dest.CountTrips,
                opt => opt.MapFrom(src => src.DriverTrips.Count));
        
        // GetTripsUser
        CreateMap<Trip, GetTripsUserResponseItem>()
            .ForMember(dest => dest.DriverName,
                opt => opt.MapFrom(src => src.Driver.FirstName))
            .ForMember(dest => dest.DriverAvatar,
                opt => opt.MapFrom(src => src.Driver.Avatar));
    }
}