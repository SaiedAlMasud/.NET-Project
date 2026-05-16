using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class AppointmentService
    {
        AppointmentRepo repo;
        IMapper mapper;

        public AppointmentService(AppointmentRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<AppointmentDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<AppointmentDTO>>(data);
            return res;
        }

        public AppointmentDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<AppointmentDTO>(data);
            return res;
        }

        public bool Create(AppointmentDTO a)
        {
            var data = mapper.Map<Appointment>(a);
            var res = repo.Create(data);
            return res;
        }

        public bool Update(AppointmentDTO a)
        {
            var data = mapper.Map<Appointment>(a);
            var res = repo.Update(data);
            return res;
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        // New methods for Patient Dashboard
        public List<AppointmentDTO> GetAppointmentsByPatientId(int patientId)
        {
            var data = repo.GetAppointmentsByPatientId(patientId);
            return mapper.Map<List<AppointmentDTO>>(data);
        }

        public bool CreateAppointment(AppointmentDTO appointment)
        {
            var data = mapper.Map<Appointment>(appointment);
            return repo.Create(data);
        }

        public bool CancelAppointment(int id)
        {
            var appointment = repo.Get(id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                return repo.Update(appointment);
            }
            return false;
        }

        // New methods for Doctor Dashboard
        public List<AppointmentDTO> GetAppointmentsByDoctorId(int doctorId)
        {
            var data = repo.GetAppointmentsByDoctorId(doctorId);
            return mapper.Map<List<AppointmentDTO>>(data);
        }

        public List<AppointmentDTO> GetTodayAppointmentsByDoctorId(int doctorId)
        {
            var data = repo.GetAppointmentsByDoctorId(doctorId);
            var today = DateTime.Today;
            var appointments = data.Where(a => a.AppointmentDateTime.Date == today).ToList();
            return mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public bool UpdateAppointmentStatus(int id, string status)
        {
            var appointment = repo.Get(id);
            if (appointment != null)
            {
                appointment.Status = status;
                return repo.Update(appointment);
            }
            return false;
        }

        // New methods for Admin Dashboard
        public List<AppointmentDTO> GetAllAppointments()
        {
            var data = repo.Get();
            return mapper.Map<List<AppointmentDTO>>(data);
        }

        public List<AppointmentDTO> GetTodayAppointments()
        {
            var data = repo.Get();
            var today = DateTime.Today;
            var appointments = data.Where(a => a.AppointmentDateTime.Date == today).ToList();
            return mapper.Map<List<AppointmentDTO>>(appointments);
        }
    }
}