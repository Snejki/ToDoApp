using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.List
{
    public class ListPutDto : ListPostDto
    {
        /// <summary>
        /// List finished status
        /// </summary>
        [Required]
        public bool IsFinished { get; set; }
    }
}
