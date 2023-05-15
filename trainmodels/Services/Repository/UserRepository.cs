using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Repository;

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
                return true;
            }
            return false;

        }

        public User GetUserByEmail(string email)
        {
            var user = _rc.users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
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