using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class UserService
    {
        private UserRepo userRepo;
        private IMapper mapper;

        public UserService(UserRepo userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public bool CreateUser(UserRegisterDTO registerDto)
        {
            if (userRepo.EmailExists(registerDto.Email))
                return false;

            var user = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                Role = registerDto.Role
            };

            return userRepo.Create(user);
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = userRepo.GetByEmail(email);
            return mapper.Map<UserDTO>(user);
        }

        public bool EmailExists(string email)
        {
            return userRepo.EmailExists(email);
        }

        public bool ValidateUserCredentials(string email, string password, out string role)
        {
            role = null;
            var user = userRepo.GetByEmail(email);

            if (user != null && user.Password == password)
            {
                role = user.Role;
                return true;
            }

            return false;
        }

        public int GetUserIdByEmail(string email)
        {
            var user = userRepo.GetByEmail(email);
            return user != null ? user.Id : 0;
        }
    }
}