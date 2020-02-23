using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.Element
{
    public class ElementGetDto
    {
        /// <summary>
        /// ID of new element
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Title of element
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Time of element creation
        /// </summary>
        public DateTime AddedAt { get; private set; }
        /// <summary>
        /// Time of element finish
        /// </summary>
        public DateTime? FinishedAt { get; private set; }
        /// <summary>
        /// Id of List that contains element
        /// </summary>
        public Guid ToDoListId { get; private set; }
    }
}
