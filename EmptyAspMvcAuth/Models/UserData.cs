namespace EmptyAspMvcAuth.Models
{
    public class UserData
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Login { get; init; }

        public string PasswordHash { get; init; }

        public UserData(string login, string password)
        {
            Login = login;
            PasswordHash = password;
        }
    }
}
