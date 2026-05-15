using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class DoctorService
    {
        DoctorRepo repo;
        IMapper mapper;
        public DoctorService(DoctorRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public List<DoctorDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<DoctorDTO>>(data);
            return res;
        }
        public DoctorDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<DoctorDTO>(data);
            return res;
        }
        public bool Create(DoctorDTO d)
        {
            var data = mapper.Map<Doctor>(d);
            var res = repo.Create(data);
            return res;

        }
        public bool Update(DoctorDTO d)
        {
            var data = mapper.Map<Doctor>(d);
            var res = repo.Update(data);
            return res;
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
