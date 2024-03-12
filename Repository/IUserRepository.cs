using ManageFinances.Entities;

namespace ManageFinances.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User Get(int id);

        User GetByUsernameOrEmail(string UsernameOrEmail);

        User Create(User user);
        User Update(User user);
        int Delete(int id);
    }
}
