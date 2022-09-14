using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommand:IRequest<CreatedGitHubProfileDto>
    {
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public CreateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                GitHubProfile? gitHubProfile = await _gitHubProfileRepository.GetAsync(c => c.UserId == request.UserId);

                await _gitHubProfileBusinessRules.GitHubProfileUrlCanNotBeDuplicatedWhenInserted(request.ProfileUrl);

                GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(request);
                GitHubProfile createdGitHubProfile = await _gitHubProfileRepository.AddAsync(mappedGitHubProfile);
                CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdGitHubProfile);
                return createdGitHubProfileDto;
            }
        }
    }
}
