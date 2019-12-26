using System;

namespace ToDo.Domain.Entities
{
    public class ToDoItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}