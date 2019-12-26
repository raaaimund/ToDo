using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Commands.CreateToDoItem
{
    public class CreateToDoItemCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public class CreateTodoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
        {
            private readonly IToDoDbContext _context;

            public CreateTodoItemCommandHandler(IToDoDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.ToDoItem
                {
                    Name = request.Name
                };

                await _context.ToDoItem.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
