using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.Element
{
    public class ElementPostDto
    {
        /// <summary>
        /// Title of element
        /// </summary>
        [Required]
        public string Title { get; private set; }
        /// <summary>
        /// ID of list that should contain element
        /// </summary>
        [Required]
        public Guid ToDoListId { get; private set; }
    }
}
