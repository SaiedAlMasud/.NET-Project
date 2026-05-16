using DAL.DbContext;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class AdminRepo
    {
        AppDbContext db;
        public AdminRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Create(Admin a)
        {
            db.Admins.Add(a);
            return db.SaveChanges() > 0;
        }
        public List<Admin> Get()
        {
            return db.Admins.ToList();
        }
        public Admin Get(int id)
        {
            return db.Admins.Find(id);
        }
        public Admin GetByUserId(int userId)
        {
            return db.Admins.FirstOrDefault(a => a.UserId == userId);
        }

        public Admin GetByEmail(string email)
        {
            return db.Admins.FirstOrDefault(a => a.Email == email);
        }
        public bool Update(Admin p)
        {
            var exobj = Get(p.Id);
            db.Entry(exobj).CurrentValues.SetValues(p);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Admins.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
