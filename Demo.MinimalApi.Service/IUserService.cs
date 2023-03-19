using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.MinimalApi.Service
{
    public interface IUserService
    {
        public Task<string> GetUserNameAsync();
    }
}
