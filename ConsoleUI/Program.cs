// See https://aka.ms/new-console-template for more information

using ConsoleUI;
using DataAccess;
using DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

#region SetUp

IHost app = Host.CreateDefaultBuilder()
.ConfigureServices((context, services) =>
{
    services.AddDataAccess(context.Configuration);
    services.AddServices(context.Configuration);

}).Build();

#endregion

var _userService = app.Services.GetRequiredService<IUserService>();

//User userCreated = await someService.CreateUser();
User userCreated = await _userService.Create(new User()
{
    Email = "la.fowks@gmail.com",
    UserName = "lfowks"
});
NewUser newUser = new(userCreated.Id, userCreated.Email, userCreated.UserName);

Console.WriteLine(newUser);