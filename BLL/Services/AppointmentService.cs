using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
