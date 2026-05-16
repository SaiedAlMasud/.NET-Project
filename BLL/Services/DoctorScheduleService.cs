using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class DoctorScheduleService
    {
        DoctorScheduleRepo repo;
        IMapper mapper;

        public DoctorScheduleService(DoctorScheduleRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<DoctorScheduleDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<DoctorScheduleDTO>>(data);
            return res;
        }

        public DoctorScheduleDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<DoctorScheduleDTO>(data);
            return res;
        }

        public bool Create(DoctorScheduleDTO ds)
        {
            var data = mapper.Map<DoctorSchedule>(ds);
            var res = repo.Create(data);
            return res;
        }

        public bool Update(DoctorScheduleDTO ds)
        {
            var data = mapper.Map<DoctorSchedule>(ds);
            var res = repo.Update(data);
            return res;
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        // New methods for Doctor Dashboard
        public List<DoctorScheduleDTO> GetSchedulesByDoctorId(int doctorId)
        {
            var data = repo.GetSchedulesByDoctorId(doctorId);
            return mapper.Map<List<DoctorScheduleDTO>>(data);
        }

        public DoctorScheduleDTO GetScheduleByDoctorAndDay(int doctorId, int dayOfWeek)
        {
            var data = repo.GetScheduleByDoctorAndDay(doctorId, dayOfWeek);
            return mapper.Map<DoctorScheduleDTO>(data);
        }

        public bool CreateOrUpdateSchedule(DoctorScheduleDTO schedule)
        {
            var existing = repo.GetScheduleByDoctorAndDay(schedule.DoctorId, schedule.DayOfWeek);
            if (existing != null)
            {
                schedule.Id = existing.Id;
                var data = mapper.Map<DoctorSchedule>(schedule);
                return repo.Update(data);
            }
            else
            {
                var data = mapper.Map<DoctorSchedule>(schedule);
                return repo.Create(data);
            }
        }

        public bool DeleteScheduleByDoctorAndDay(int doctorId, int dayOfWeek)
        {
            var schedule = repo.GetScheduleByDoctorAndDay(doctorId, dayOfWeek);
            if (schedule != null)
            {
                return repo.Delete(schedule.Id);
            }
            return false;
        }
    }
}