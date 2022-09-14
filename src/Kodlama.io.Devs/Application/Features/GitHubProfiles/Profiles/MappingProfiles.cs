using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();

            CreateMap<IPaginate<GitHubProfile>, GitHubProfileListModel>().ReverseMap();
            CreateMap<GitHubProfile, GitHubProfileListDto>().ForMember(m => m.FirstName, opt => opt
                .MapFrom(m => m.User.FirstName)).ReverseMap();
            CreateMap<GitHubProfile, GitHubProfileListDto>().ForMember(m => m.LastName, opt => opt
                .MapFrom(m => m.User.LastName)).ReverseMap();
        }
    }
}
