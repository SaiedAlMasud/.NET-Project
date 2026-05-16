using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DbContext;
using DAL.Entities;

namespace DAL.Repos
{
    public class UserRepo
    {
        private AppDbContext db;

        public UserRepo(AppDbContext db)
        {
            this.db = db;
        }

        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public User GetByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool EmailExists(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }

        public bool Authenticate(string email, string password)
        {
            return db.Users.Any(u => u.Email == email && u.Password == password);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public bool Delete(int id)
        {
            var user = Get(id);
            if (user == null) return false;

            db.Users.Remove(user);
            return db.SaveChanges() > 0;
        }
    }
}