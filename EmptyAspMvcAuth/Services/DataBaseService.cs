using EmptyAspMvcAuth.Models;

namespace EmptyAspMvcAuth.Services
{
    // It's not really a service, but even if you decide to add one, please don't forget about interfaces
    public class DataBaseService
    {
        // You should never use attributes directly. Consider swapping this to property
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
