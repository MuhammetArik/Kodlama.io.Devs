using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class UserLoginQuery : UserForLoginDto, IRequest<AccessToken>
    {
        public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public UserLoginQueryHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(UserLoginQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(c => c.Email.ToLower() == request.Email.ToLower(),
                    include: c => c.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim));

                _userBusinessRules.UserShouldExist(user);
                _userBusinessRules.UserCredentialsShouldMatch(request.Password, user.PasswordHash, user.PasswordSalt);

                List<OperationClaim> operationClaims = new List<OperationClaim>();
                foreach (var operationClaim in user.UserOperationClaims)
                {
                    operationClaims.Add(operationClaim.OperationClaim);
                }

                var token = _tokenHelper.CreateToken(user, operationClaims);
                return token;
            }
        }
    }
}
