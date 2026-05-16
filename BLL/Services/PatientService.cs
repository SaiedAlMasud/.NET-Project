using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class PatientService
    {
        private PatientRepo patientRepo;
        private IMapper mapper;

        public PatientService(PatientRepo patientRepo, IMapper mapper)
        {
            this.patientRepo = patientRepo;
            this.mapper = mapper;
        }

        public bool CreatePatient(PatientRegisterDTO registerDto, int userId)
        {
            var patient = new Patient
            {
                UserId = userId,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = registerDto.Gender
            };

            return patientRepo.Create(patient);
        }

        public List<PatientDTO> GetAllPatients()
        {
            var patients = patientRepo.Get();
            return mapper.Map<List<PatientDTO>>(patients);
        }

        public PatientDTO GetPatientById(int id)
        {
            var patient = patientRepo.Get(id);
            return mapper.Map<PatientDTO>(patient);
        }

        public PatientDTO GetPatientByUserId(int userId)
        {
            var patient = patientRepo.GetByUserId(userId);
            return mapper.Map<PatientDTO>(patient);
        }

        public bool UpdatePatient(PatientDTO patientDto)
        {
            var patient = mapper.Map<Patient>(patientDto);
            return patientRepo.Update(patient);
        }

        public bool DeletePatient(int id)
        {
            return patientRepo.Delete(id);
        }
    }
}