namespace Core
{
    public class Channel : IValueObject
    {
        private Channel()
        {
        }

        public Channel(User user, string id)
        {
            Id = id;
            User = user;
            UserSecretLogin = user?.SecretLogin;
        }

        public string Id { get; private set; }

        public string UserSecretLogin { get; private set; }

        public User User { get; private set; }
    }
}
