using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Dtos.Login;
using ToDoApp.Api.Interfaces;
using ToDoApp.Api.Services;

namespace ToDoApp.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IJwthandler _jwtHandler;
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public AuthController(
            IJwthandler jwtHandler,
            IUserRepository userRepository,
            IEncrypter encrypter
            )
        {
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDto">Username and password</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginPostDto loginDto)
        {
            var user = await _userRepository.GetByUsername(loginDto.Username);
            if(user == null)
            {
                return NotFound();
            }

            var hash = _encrypter.GetHash(loginDto.Password, user.Salt);
            if(hash != user.Hash)
            {
                return NotFound();
            }

            var token = _jwtHandler.CreateToken(user.Id);

            return Ok(token);
        }
       
    }
}