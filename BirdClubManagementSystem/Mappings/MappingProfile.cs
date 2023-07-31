using AutoMapper;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;

namespace BirdClubManagementSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bird, BirdDTO>()
                .ReverseMap();

            CreateMap<Blog, BlogDTO>()
                .ReverseMap();

            CreateMap<BlogCategory, BlogCategoryDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentDTO>()
                .ReverseMap();

            CreateMap<Feedback, FeedbackDTO>()
                .ReverseMap();

            CreateMap<FieldTrip, FieldTripDTO>()
                .ReverseMap();

            CreateMap<FieldTripRegistration, FieldTripRegistrationDTO>()
                .ReverseMap();

            CreateMap<LoginCredential, LoginCredentialDTO>()
                .ReverseMap();

            CreateMap<Meeting, MeetingDTO>()
                .ReverseMap();

            CreateMap<MeetingRegistration, MeetingRegistrationDTO>()
                .ReverseMap();

            CreateMap<MembershipRequest, MembershipRequestDTO>()
                .ReverseMap();

            CreateMap<Tournament, TournamentDTO>()
                .ReverseMap();

            CreateMap<TournamentRegistration, TournamentRegistrationDTO>()
                .ReverseMap();

            CreateMap<TournamentStanding, TournamentStandingDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
