using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface IUser
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        bool DeleteUser(int id);
    }
}