using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface IUser
    {
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        bool DeleteUser(int id);
    }
}