using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class DoctorService
    {
        private DoctorRepo doctorRepo;
        private IMapper mapper;

        public DoctorService(DoctorRepo doctorRepo, IMapper mapper)
        {
            this.doctorRepo = doctorRepo;
            this.mapper = mapper;
        }

        public bool CreateDoctor(DoctorRegisterDTO registerDto, int userId)
        {
            var doctor = new Doctor
            {
                UserId = userId,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Specialization = registerDto.Specialization,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                ConsultationFee = registerDto.ConsultationFee
            };

            return doctorRepo.Create(doctor);
        }

        public List<DoctorDTO> GetAllDoctors()
        {
            var doctors = doctorRepo.Get();
            return mapper.Map<List<DoctorDTO>>(doctors);
        }

        public DoctorDTO GetDoctorById(int id)
        {
            var doctor = doctorRepo.Get(id);
            return mapper.Map<DoctorDTO>(doctor);
        }

        public DoctorDTO GetDoctorByUserId(int userId)
        {
            var doctor = doctorRepo.GetByUserId(userId);
            return mapper.Map<DoctorDTO>(doctor);
        }

        public bool UpdateDoctor(DoctorDTO doctorDto)
        {
            var doctor = mapper.Map<Doctor>(doctorDto);
            return doctorRepo.Update(doctor);
        }

        public bool DeleteDoctor(int id)
        {
            return doctorRepo.Delete(id);
        }
    }
}