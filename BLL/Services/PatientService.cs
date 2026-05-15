using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class PatientService
    {
        PatientRepo repo;
        IMapper mapper;
        public PatientService(PatientRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public List<PatientDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<PatientDTO>>(data);
            return res;
        }
        public PatientDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<PatientDTO>(data);
            return res;
        }
        public bool Create(PatientDTO p)
        {
            var data = mapper.Map<Patient>(p);
            var res = repo.Create(data);
            return res;

        }
        public bool Update(PatientDTO p)
        {
            var data = mapper.Map<Patient>(p);
            var res = repo.Update(data);
            return res;
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

    }
}
