using System;

namespace ToDo.Dto
{
    public class ToDoItem : IDto<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}