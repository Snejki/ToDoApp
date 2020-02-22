using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Dtos.List;
using ToDoApp.Api.Interfaces;
using ToDoApp.Api.Repositories;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Controllers
{
    [Route("api/list")]
    public class ListController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IToDoListRepository _listRepository;
        private readonly IMapper _mapper;


        public ListController(
            IUserRepository userRepository,
            IToDoListRepository listRepository,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _listRepository = listRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool? isFinished, string phrase = "")
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var toDoLists = await _listRepository.GetForUser(AuthUserId, phrase, isFinished);

            var listsDto = _mapper.Map<ICollection<ListGetDto>>(toDoLists);

            return Ok(listsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var toDoList = await _listRepository.GetById(AuthUserId);

            if(toDoList == null)
            {
                return NotFound();
            }

            if(toDoList.UserId != AuthUserId)
            {
                return BadRequest();
            }

            var lissDto = _mapper.Map<ListGetDto>(toDoList);

            return Ok(lissDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ListPostDto dto)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if(user == null)
            {
                return Unauthorized();
            }

            var id = Guid.NewGuid();
            var addedAt = DateTime.UtcNow;
            var list = new ToDoList(id, user.Id, dto.Title, dto.Color, addedAt);

            if(!(await _listRepository.Add(list)))
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var toDoList = await _listRepository.GetById(AuthUserId);

            if (toDoList == null)
            {
                return NotFound();
            }

            if(!(await _listRepository.Remove(toDoList)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}