using System;

namespace ToDoApp.Api.Dtos.List
{
    public class ListGetDto
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime AddedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public string Color { get; private set; }
    }
}
