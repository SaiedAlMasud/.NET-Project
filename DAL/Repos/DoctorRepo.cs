using DAL.DbContext;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class DoctorRepo
    {
        AppDbContext db;
        public DoctorRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Create(Doctor d)
        {
            db.Doctors.Add(d);
            return db.SaveChanges() > 0;
        }
        public List<Doctor> Get()
        {
            return db.Doctors.ToList();
        }
        public Doctor Get(int id)
        {
            return db.Doctors.Find(id);
        }
        public Doctor GetByUserId(int userId)
        {
            return db.Doctors.FirstOrDefault(d => d.UserId == userId);
        }

        public Doctor GetByEmail(string email)
        {
            return db.Doctors.FirstOrDefault(d => d.Email == email);
        }
        public bool Update(Doctor d)
        {
            var exobj = Get(d.Id);
            db.Entry(exobj).CurrentValues.SetValues(d);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Doctors.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}