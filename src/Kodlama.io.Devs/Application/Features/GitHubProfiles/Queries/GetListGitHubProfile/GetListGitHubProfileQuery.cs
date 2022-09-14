using Application.Features.GitHubProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Queries.GetListGitHubProfile
{
    public class GetListGitHubProfileQuery : IRequest<GitHubProfileListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListGitHubProfileQueryHandler : IRequestHandler<GetListGitHubProfileQuery, GitHubProfileListModel>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;

            public GetListGitHubProfileQueryHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
            }

            public async Task<GitHubProfileListModel> Handle(GetListGitHubProfileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GitHubProfile> gitHubProfile = await _gitHubProfileRepository.GetListAsync(
                                                                        include: m => m.Include(m => m.User),
                                                                        index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                GitHubProfileListModel mappedgitHubProfileListModel = _mapper.Map<GitHubProfileListModel>(gitHubProfile);
                return mappedgitHubProfileListModel;
            }
        }
    }
}
