using AutoMapper;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Statuses;

namespace BirdClubManagementSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bird, BirdDTO>();
            CreateMap<Blog, BlogDTO>()
                .ForMember(dest => dest.BlogCategory,
                    src => src.MapFrom(src => src.BlogCategory.Name));
            CreateMap<BlogCategory, BlogCategoryDTO>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.ProfilePicture,
                    src => src.MapFrom(src => src.User.ProfilePicture));
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<FieldTrip, FieldTripDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<FieldTripRegistration, FieldTripRegistrationDTO>();
            CreateMap<LoginCredential, LoginCredentialDTO>();
            CreateMap<Meeting, MeetingDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<MeetingRegistration, MeetingRegistrationDTO>();
            CreateMap<MembershipRequest, MembershipRequestDTO>();
            CreateMap<Tournament, TournamentDTO>()
                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => EventStatuses.Convert(src.Status)));
            CreateMap<TournamentRegistration, TournamentRegistrationDTO>();
            CreateMap<TournamentStanding, TournamentStandingDTO>();
        }
    }
}
