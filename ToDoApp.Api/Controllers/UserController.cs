using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Dtos.User;
using ToDoApp.Api.Interfaces;
using ToDoApp.Api.Services;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserController(
            IUserRepository userRepository,
            IEncrypter encrypter
            )
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserPostDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email);
            if(user != null)
            {
                return BadRequest(); 
            }

            user = await _userRepository.GetByUsername(dto.Username);
            if(user != null)
            {
                return BadRequest();
            }

            var id = Guid.NewGuid();
            var addedAt = DateTime.UtcNow;
            var salt = _encrypter.GetSalt(dto.Password);
            var hash = _encrypter.GetHash(dto.Password, salt);
            user = new User(id, dto.Username, dto.Email, hash, salt, addedAt);

            if(await _userRepository.Add(user))
            {
                return Ok(new { id = user.Id });
            }

            return BadRequest();
        }
    }
}