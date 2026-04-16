using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<bool> AddUserAsync(User newUser);
        Task<List<User>> GetAllUsersAsync();
        Task<User> VerifyUserAsync(string userName, string passWord);

    }
}
