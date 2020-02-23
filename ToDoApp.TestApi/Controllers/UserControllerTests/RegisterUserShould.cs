using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using ToDoApp.Api.Controllers;
using ToDoApp.Api.Dtos.User;
using ToDoApp.Api.Repositories;
using ToDoApp.Api.Services;
using Xunit;

namespace ToDoApp.TestApi.Controllers.UserControllerTests
{
    public class RegisterUserShould
    {

        [Theory]
        [InlineData("test@gmail.com","TestPassword","TestowyUser")]
        [InlineData("test2@gmail.com","TestPassword","TestUser")]
        public async Task CreateNewUser(string email, string password, string username)
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<Encrypter>();

            var userController = new UserController(
                userRepositoryMock.Object,
                encrypterMock.Object,
                mapperMock.Object);

            var user = new UserPostDto
            {
                Email = email,
                Password = password,
                Username = username
            };

            var result = await userController.RegisterUser(user);

            Assert.IsType<CreatedAtRouteResult>(result);
        }
    }
}
