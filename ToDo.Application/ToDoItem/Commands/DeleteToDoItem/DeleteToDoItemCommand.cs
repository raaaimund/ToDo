using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Commands.DeleteToDoItem
{
    public class DeleteToDoItemCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
        {
            private readonly IToDoDbContext _context;

            public DeleteToDoItemCommandHandler(IToDoDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ToDoItem.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ToDoItem), request.Id);
                }

                _context.ToDoItem.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
