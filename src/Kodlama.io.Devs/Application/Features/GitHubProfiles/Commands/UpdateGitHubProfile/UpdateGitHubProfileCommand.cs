using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand:IRequest<UpdatedGitHubProfileDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                await _gitHubProfileBusinessRules.GitHubProfileUrlCanNotBeDuplicatedWhenInserted(request.ProfileUrl);

                GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(request);
                GitHubProfile updatedTechnology = await _gitHubProfileRepository.UpdateAsync(mappedGitHubProfile);
                UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedTechnology);
                return updatedGitHubProfileDto;

            }
        }
    }
}
