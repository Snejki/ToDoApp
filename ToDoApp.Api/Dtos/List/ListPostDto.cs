using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.List
{
    public class ListPostDto
    {
        /// <summary>
        /// Title of list
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Background color of list
        /// </summary>
        [Required]
        public string Color { get; set; }
    }
}
