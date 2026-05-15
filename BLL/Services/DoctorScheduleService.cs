using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
