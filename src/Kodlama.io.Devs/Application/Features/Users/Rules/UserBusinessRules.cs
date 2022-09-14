using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void UserShouldExist(User user)
        {
            if (user == null) throw new BusinessException("User does not exist");
        }

        public void UserCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("User credentials does not match");
        }

        public async Task EmailNameCanNotBeDuplicatedWhenInserted(string email)
        {
            var result = await _userRepository.GetAsync(u => u.Email.ToLower().Equals(email.ToLower()));
            if (result != null) throw new BusinessException("Email adress exist");
        }
    }
}
