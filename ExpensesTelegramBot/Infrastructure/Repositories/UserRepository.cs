using Core;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(User newUser)
            => context.Add(newUser);

        public void Update(User userToUpdate)
            => context.Users.Update(userToUpdate);

        public User GetById(int id)
            => context.Users.Find(id);

        public void Remove(User userToRemove)
            => context.Users.Remove(userToRemove);
    }
}