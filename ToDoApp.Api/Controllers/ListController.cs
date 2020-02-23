using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Dtos.List;
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


        /// <summary>
        /// Get list of ToDoList
        /// </summary>
        /// <param name="isFinished">Is ToDoList finished </param>
        /// <param name="phrase">Phrase for filter ToDoList</param>
        /// <param name="page">Size of page</param>
        /// <param name="pageSize">Number of page</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get(bool? isFinished, string phrase = "", int page = 1, int pageSize = 5)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var pagesCount = await _listRepository.CountPages(AuthUserId, phrase, isFinished, pageSize);
            if(page > pagesCount)
            {
                page = pagesCount;
            }

            var toDoLists = await _listRepository.GetForUser(AuthUserId, phrase, isFinished, page, pageSize);
            var listsDto = _mapper.Map<ICollection<ListGetDto>>(toDoLists);

            AddPaginationInfo(page, pagesCount);
            return Ok(listsDto);
        }

        /// <summary>
        /// Get single ToDoList
        /// </summary>
        /// <param name="id">ID of ToDoList</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetList")]
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
                return Forbid();
            }

            var lissDto = _mapper.Map<ListGetDto>(toDoList);

            return Ok(lissDto);
        }

        /// <summary>
        /// Create new ToDoList
        /// </summary>
        /// <param name="dto">data of ToDoList</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            await _listRepository.Add(list);
            return CreatedAtRoute("GetList", new { id = id }, list);
        }

        /// <summary>
        /// Update ToDoList
        /// </summary>
        /// <param name="id">ID of ToDoList</param>
        /// <param name="listDto">data to update</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, ListPutDto listDto)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var list = await _listRepository.GetById(id);
            if(list == null)
            {
                return NotFound();
            }

            if(list.UserId != user.Id)
            {
                return Forbid();
            }

            DateTime? finishedAt = null;
            if(listDto.IsFinished)
            {
                finishedAt = DateTime.UtcNow;
            }

            list.SetFinishedAt(finishedAt);
            list.SetColor(listDto.Color);
            list.SetTitle(listDto.Title);
            await _listRepository.Update(list);

            return NoContent();
        }

        /// <summary>
        /// Delete ToDoList
        /// </summary>
        /// <param name="id">ID of ToDoList</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            await _listRepository.Remove(toDoList);
            return NoContent();
        }
    }
}