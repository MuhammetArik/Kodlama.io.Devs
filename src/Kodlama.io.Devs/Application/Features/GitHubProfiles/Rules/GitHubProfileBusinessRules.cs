using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository= gitHubProfileRepository;
        }

        public async Task GitHubProfileUrlCanNotBeDuplicatedWhenInserted(string profileUrl)
        {
            IPaginate<GitHubProfile> result = await _gitHubProfileRepository.GetListAsync(t => t.ProfileUrl == profileUrl);
            if (result.Items.Any()) throw new BusinessException("Github profile url exists.");
        }

        public async Task GitHubProfileShouldExistWhenRequested(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested user does not exist.");
        }
    }
}
