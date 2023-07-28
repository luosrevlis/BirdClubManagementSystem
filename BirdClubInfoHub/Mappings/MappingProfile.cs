using AutoMapper;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;

namespace BirdClubInfoHub.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bird, BirdDTO>()
                .ReverseMap();

            CreateMap<Blog, BlogDTO>()
                .ForMember(dest => dest.BlogCategory,
                    src => src.MapFrom(src => src.BlogCategory.Name));
            CreateMap<BlogDTO, Blog>();

            CreateMap<BlogCategory, BlogCategoryDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentDTO>()
                .ReverseMap();
            CreateMap<CommentDTO, Comment>();

            CreateMap<Feedback, FeedbackDTO>()
                .ReverseMap();

            CreateMap<FieldTrip, FieldTripDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<FieldTripDTO, FieldTrip>();

            CreateMap<FieldTripRegistration, FieldTripRegistrationDTO>()
                .ReverseMap();

            CreateMap<LoginCredential, LoginCredentialDTO>()
                .ReverseMap();

            CreateMap<Meeting, MeetingDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<MeetingDTO, Meeting>();

            CreateMap<MeetingRegistration, MeetingRegistrationDTO>()
                .ReverseMap();

            CreateMap<MembershipRequest, MembershipRequestDTO>()
                .ReverseMap();

            CreateMap<Tournament, TournamentDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<TournamentDTO, Tournament>();

            CreateMap<TournamentRegistration, TournamentRegistrationDTO>()
                .ReverseMap();

            CreateMap<TournamentStanding, TournamentStandingDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role,
                    src => src.MapFrom(src => UserRoles.Convert(src.Role)));
            CreateMap<UserDTO, User>();
        }
    }
}
