using DAL.Entities;
using DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class DoctorScheduleRepo
    {
        AppDbContext db;
        public DoctorScheduleRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Create(DoctorSchedule ds)
        {
            db.DoctorSchedules.Add(ds);
            return db.SaveChanges() > 0;
        }
        public List<DoctorSchedule> Get()
        {
            return db.DoctorSchedules.ToList();
        }
        public DoctorSchedule Get(int id)
        {
            return db.DoctorSchedules.Find(id);
        }
        public bool Update(DoctorSchedule ds)
        {
            var exobj = Get(ds.Id);
            db.Entry(exobj).CurrentValues.SetValues(ds);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.DoctorSchedules.Remove(exobj);
            return db.SaveChanges() > 0;
        }
        public List<DoctorSchedule> GetSchedulesByDoctorId(int doctorId)
        {
            return db.DoctorSchedules.Where(ds => ds.DoctorId == doctorId).ToList();
        }

        public DoctorSchedule GetScheduleByDoctorAndDay(int doctorId, int dayOfWeek)
        {
            return db.DoctorSchedules.FirstOrDefault(ds => ds.DoctorId == doctorId && ds.DayOfWeek == dayOfWeek);
        }
    }
}
