using System.Text.RegularExpressions;
using System.Text;
using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Repository;
using Microsoft.EntityFrameworkCore;

namespace trainmodels.Services.Repository
{
    public class UserRepository : IUser
    {
        private readonly RailContext _rc;
        public UserRepository(RailContext rc)
        {
            _rc = rc;
        }
        public void AddUser(User user)
        {
            _rc.users.Add(user);
            _rc.SaveChanges();
        }

        public bool DeleteUser(int id)
        {
            var user = _rc.users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _rc.users.Remove(user);
                _rc.SaveChanges();
                return true;
            }
            return false;

        }

        public User GetUserById(int id)
        {
            var user = _rc.users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            _rc.users.Update(user);
            _rc.SaveChanges();
        }

    }
}
