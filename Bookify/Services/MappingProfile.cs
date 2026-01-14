using AutoMapper;
using Bookify.DTOs.Medias;
using Bookify.DTOs.Reservations;
using Bookify.DTOs.Rooms;
using Bookify.DTOs.RoomTypes;
using Bookify.Models;

namespace Bookify.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ----- ROOMTYPE -----
            CreateMap<RoomType, RoomTypeReadDTO>();
            CreateMap<RoomTypeCreateDTO, RoomType>();

            // ----- ROOM -----
            CreateMap<RoomCreateDTO, Room>();
            CreateMap<Room, RoomReadDTO>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType != null ? src.RoomType.Name : string.Empty)); 

            // ----- MEDIA -----
            CreateMap<Media, MediaReadDTO>()
                .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => $"/api/Media/{src.Id}/file"));

            CreateMap<MediaCreateDTO, Media>()
                .ForMember(dest => dest.Data, opt => opt.Ignore())
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForMember(dest => dest.ContentType, opt => opt.Ignore());

            //----- RESERVATION -----
            CreateMap<Reservation, ReservationReadDTO>()
                 .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.RoomNumber.ToString() : string.Empty)) 
                 .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : string.Empty))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<ReservationCreateDTO, Reservation>()
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ReservationStatus.Pending));
        }
    }
}