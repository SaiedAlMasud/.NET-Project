using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AdminService
    {
        private AdminRepo adminRepo;
        private IMapper mapper;

        public AdminService(AdminRepo adminRepo, IMapper mapper)
        {
            this.adminRepo = adminRepo;
            this.mapper = mapper;
        }

        public bool CreateAdmin(AdminRegisterDTO registerDto, int userId)
        {
            var admin = new Admin
            {
                UserId = userId,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = registerDto.Gender
            };

            return adminRepo.Create(admin);
        }

        public List<AdminDTO> GetAllPatients()
        {
            var patients = adminRepo.Get();
            return mapper.Map<List<AdminDTO>>(patients);
        }

        public AdminDTO GetAdminById(int id)
        {
            var admin = adminRepo.Get(id);
            return mapper.Map<AdminDTO>(admin);
        }

        public AdminDTO GetAdminByUserId(int userId)
        {
            var admin = adminRepo.GetByUserId(userId);
            return mapper.Map<AdminDTO>(admin);
        }

        public bool UpdateAdmin(AdminDTO AdminDTO)
        {
            var admin = mapper.Map<Admin>(AdminDTO);
            return adminRepo.Update(admin);
        }

        public bool DeleteAdmin(int id)
        {
            return adminRepo.Delete(id);
        }
    }
}
