using DAL.DbContext;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class PatientRepo
    {
        AppDbContext db;
        public PatientRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Create(Patient p)
        {
            db.Patients.Add(p);
            return db.SaveChanges() > 0;
        }
        public List<Patient> Get()
        {
            return db.Patients.ToList();
        }
        public Patient Get(int id)
        {
            return db.Patients.Find(id);
        }
        public Patient GetByUserId(int userId)
        {
            return db.Patients.FirstOrDefault(p => p.UserId == userId);
        }

        public Patient GetByEmail(string email)
        {
            return db.Patients.FirstOrDefault(p => p.Email == email);
        }
        public bool Update(Patient p)
        {
            var exobj = Get(p.Id);
            db.Entry(exobj).CurrentValues.SetValues(p);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Patients.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}