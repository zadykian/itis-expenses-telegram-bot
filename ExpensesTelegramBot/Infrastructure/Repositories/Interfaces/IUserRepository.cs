using Core;

namespace Infrastructure
{
    public interface IUserRepository
    {
        void Add(User newUser);
        void Update(User userToUpdate);
        void Remove(User userToRemove);
        User GetById(int id);
    }
}
