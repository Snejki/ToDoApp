using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Api.Dtos.Element;
using ToDoApp.Api.Repositories;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Controllers
{
    [Route("api/element")]
    public class ElementController : BaseController
    {
        private readonly IElementRepository _elementRepository;
        private readonly IToDoListRepository _listRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ElementController(
            IElementRepository elementRepository,
            IToDoListRepository listRepository,
            IUserRepository userRepository, 
            IMapper mapper
            )
        {
            _elementRepository = elementRepository;
            _listRepository = listRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of element
        /// </summary>
        /// <param name="isFinished">Is element finished filter</param>
        /// <param name="phrase">Phrase for filter element</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<ICollection<ElementGetDto>>> Get(bool? isFinished, string phrase = "")
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var elements = await _elementRepository.GetForUser(AuthUserId, phrase, isFinished);

            var elementsDto = _mapper.Map<ICollection<ElementGetDto>>(elements);

            return Ok(elementsDto);
        }


        /// <summary>
        /// Get single element
        /// </summary>
        /// <param name="id">Id of element</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}", Name = "GetElement")]
        public async Task<ActionResult<ElementGetDto>> Get(Guid id)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var element = await _elementRepository.GetById(id);
            if (element == null)
            {
                return NotFound();
            }

            if (element.ToDoList.UserId != user.Id)
            {
                return Forbid();
            }

            var elementDto = _mapper.Map<ElementGetDto>(element);
            return Ok(elementDto);
        }

        /// <summary>
        /// Create new element
        /// </summary>
        /// <param name="elementDto">data of new element</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> Post(ElementPostDto elementDto)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var toDoList = await _listRepository.GetById(elementDto.ToDoListId);
            if(toDoList == null)
            {
                return BadRequest();
            }

            if(toDoList.UserId != user.Id)
            {
                return Forbid();
            }

            var id = Guid.NewGuid();
            var addedAt = DateTime.UtcNow;
            var element = new ToDoElement(id, toDoList.Id, elementDto.Title, addedAt);
            await _elementRepository.Add(element);
            return CreatedAtRoute("GetElement", new { id = id }, element);
        }

        /// <summary>
        /// Edit element
        /// </summary>
        /// <param name="id">ID of element</param>
        /// <param name="elementDto">data to update</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, ElementPutDto elementDto)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var element = await _elementRepository.GetById(id);
            if(element == null)
            {
                return NotFound();
            }

            var toDoList = await _listRepository.GetById(element.ToDoListId);
            if (toDoList == null)
            {
                return BadRequest();
            }

            DateTime? finishedAt = null;
            if (elementDto.IsFinished)
            {
                finishedAt = DateTime.UtcNow;
            }

            element.SetFinishedAt(finishedAt);
            element.SetTitle(elementDto.Title);
            await _elementRepository.Update(element);
            return NoContent();
        }

        /// <summary>
        /// Delete element
        /// </summary>
        /// <param name="id">Id of element to delete</param>
        /// <returns>ActionResult</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetById(AuthUserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var element = await _elementRepository.GetById(id);
            if (element == null)
            {
                return NotFound();
            }

            var toDoList = await _listRepository.GetById(element.ToDoListId);
            if (toDoList == null)
            {
                return BadRequest();
            }

            await _elementRepository.Remove(element);
            return NoContent();
        }
    }
}