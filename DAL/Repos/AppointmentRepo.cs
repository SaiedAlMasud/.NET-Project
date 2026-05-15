using DAL.Entities;
using DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class AppointmentRepo
    {
        AppDbContext db;
        public AppointmentRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Create(Appointment a)
        {
            db.Appointments.Add(a);
            return db.SaveChanges() > 0;
        }
        public List<Appointment> Get()
        {
            return db.Appointments.ToList();
        }
        public Appointment Get(int id)
        {
            return db.Appointments.Find(id);
        }
        public bool Update(Appointment a)
        {
            var exobj = Get(a.Id);
            db.Entry(exobj).CurrentValues.SetValues(a);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Appointments.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
