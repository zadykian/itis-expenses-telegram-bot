namespace Application
{
    public class UserLoginPassword
    {
        public UserLoginPassword(string login, string password)
        {
            Login = login.ToLowerInvariant();
            Password = password;
        }

        public string Login { get; }

        public string Password { get; }
    }
}
