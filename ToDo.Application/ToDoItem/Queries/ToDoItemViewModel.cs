using System;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Queries
{
    public class ToDoItemViewModel : IMap<Domain.Entities.ToDoItem, ToDoItemViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ToDoItemViewModel Map(Domain.Entities.ToDoItem from)
        {
            Id = from.Id;
            Name = from.Name;
            return this;
        }
    }
}
