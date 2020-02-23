using System;
using System.Collections.Generic;
using ToDoApp.Api.Dtos.Element;

namespace ToDoApp.Api.Dtos.List
{
    public class ListGetDto
    {
        /// <summary>
        /// ID of list
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Title of list
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Time of lsit creation
        /// </summary>
        public DateTime AddedAt { get; private set; }
        /// <summary>
        /// Time of list finish
        /// </summary>
        public DateTime? FinishedAt { get; private set; }
        /// <summary>
        /// background color of list
        /// </summary>
        public string Color { get; private set; }
        /// <summary>
        /// List of elements in list
        /// </summary>
        public virtual List<ElementGetDto> Elements { get; set; }
    }
}
