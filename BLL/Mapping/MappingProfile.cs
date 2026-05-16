using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admin, AdminDTO>().ReverseMap();
            CreateMap<Admin, AdminRegisterDTO>().ReverseMap();

            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Patient, PatientRegisterDTO>().ReverseMap();

            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Doctor, DoctorRegisterDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            
            
        }
    }
}