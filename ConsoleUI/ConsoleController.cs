using DataAccess.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public record NewUser(int id, string email, string userName);
    public class ConsoleController
    {
        private readonly IUserService _userService;

        public ConsoleController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> CreateUser()
        {
            User createdUser = await _userService.Create(new User()
            {
                Email = "la.fowks@gmail.com",
                UserName = "lfowks"
            });

            return createdUser;
        }
    }
}
