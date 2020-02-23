using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.Element
{
    public class ElementPutDto
    {
        /// <summary>
        /// Title of element
        /// </summary>
        [Required]
        public string Title { get; private set; }
        /// <summary>
        /// Finish status of element
        /// </summary>
        [Required]
        public bool IsFinished { get; set; }
    }
}
