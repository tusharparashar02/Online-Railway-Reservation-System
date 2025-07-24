using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<TrainScheduleDto, TrainSchedule>().ReverseMap();
        CreateMap<PaymentDetailDto, PaymentDetail>().ReverseMap();
        CreateMap<UserDto, ApplicationUser>();
        CreateMap<ReservationCreateDto, Reservation>()
    .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
        CreateMap<Reservation, ReservationDto>()
        .ForMember(dest => dest.TravelDate, opt => opt.MapFrom(src => src.TrainSchedule.TravelDate))
        .ForMember(dest => dest.Train, opt => opt.MapFrom(src => src.TrainSchedule.Train))
        .ForMember(dest => dest.PNR, opt => opt.MapFrom(src => src.PNR)) // ➕ Add PNR
        .ForMember(dest => dest.Passengers, opt => opt.MapFrom(src => src.Passengers)) // ➕ Add passengers
        .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment)); // ➕ Add payment

        CreateMap<PassengerDetailDto, PassengerDetail>()
            .ForMember(dest => dest.ReservationId, opt => opt.Ignore()) // ✅ This prevents unwanted FK mapping
            .ReverseMap();

        CreateMap<Train, TrainDto>().ReverseMap();
        CreateMap<Fare, FareDto>().ReverseMap();
        CreateMap<PaymentCreateDto, PaymentDetail>(); // Input mapping
        CreateMap<CateringOrder, CateringOrderDto>().ReverseMap();
        CreateMap<WellnessKitRequest, WellnessKitRequestDto>().ReverseMap(); // for later

    }
}