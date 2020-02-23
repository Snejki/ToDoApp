using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Dtos.User;
using ToDoApp.Api.Repositories;
using ToDoApp.Api.Services;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IEncrypter encrypter,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="dto">data of user to be registred</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserPostDto dto)
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

            await _userRepository.Add(user);
            return CreatedAtRoute("GetActiveUser", user);
        }

        /// <summary>
        /// Get  data of logged user
        /// </summary>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetActiveUser")]
        public async Task<ActionResult<UserGetDto>> Get()
        {
            var user = await _userRepository.GetById(AuthUserId);
            if(user == null)
            {
                return Unauthorized();
            }

            var userDto = _mapper.Map<UserGetDto>(user);
            return Ok(userDto);
        }

        /// <summary>
        /// Change password of logged user
        /// </summary>
        /// <param name="passwordDto">password data</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("changepassword")]
        public async Task<ActionResult> ChangePassword(UserChangePasswordDto passwordDto)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var currencthash = _encrypter.GetHash(passwordDto.CurrentPassword, user.Salt);
            if(currencthash != user.Hash)
            {
                return BadRequest();
            }

            var newSalt = _encrypter.GetSalt(passwordDto.NewPassword);
            var newHash = _encrypter.GetHash(passwordDto.NewPassword, newSalt);

            user.SetPassword(newHash, newSalt);
            await _userRepository.Update(user);

            return NoContent();
        }


        /// <summary>
        /// Delete logged user
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete()
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            await _userRepository.Remove(user);
            return NoContent();
        }
    }
}