using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL
{
    public class MapperConfig
    {
        public static AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => {
            cfg.CreateMap<Patient, PatientDTO>().ReverseMap();
            cfg.CreateMap<Doctor, DoctorDTO>().ReverseMap();
            cfg.CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            cfg.CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();

        });

        public static IMapper GetMapper()
        {
            return config.CreateMapper();
        }
    }
}
