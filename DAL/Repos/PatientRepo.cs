using DAL.Entities;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class PatientRepo
    {
        AppointmentDBContext db;
        public PatientRepo(AppointmentDBContext db)
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