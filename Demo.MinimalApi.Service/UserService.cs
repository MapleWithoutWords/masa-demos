using Microsoft.AspNetCore.Builder;

namespace Demo.MinimalApi.Service
{
    public class UserService : ServiceBase, IUserService
    {
        public async Task<string> GetUserNameAsync()
        {
            return "userName";
        }
    }
}