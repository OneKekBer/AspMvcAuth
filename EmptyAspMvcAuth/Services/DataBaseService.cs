using EmptyAspMvcAuth.Models;

namespace EmptyAspMvcAuth.Services
{
    public class DataBaseService
    {
        public List<UserData> users = new List<UserData>();

        public bool IsLoginExists(string login)
        {
            foreach (var item in users)
            {
                if (item.Login == login) // бля сравнивать строки == просто гениально
                {
                    return true;
                }
            }
            return false;
        }

        public UserData FindUserByLogin(string login)
        {
            foreach (var item in users)
            {
                if (item.Login == login) // бля сравнивать строки == просто гениально
                {
                    return item;
                }
            }
            throw new Exception($"User with this login:{login} not found");
        }
    }
}
