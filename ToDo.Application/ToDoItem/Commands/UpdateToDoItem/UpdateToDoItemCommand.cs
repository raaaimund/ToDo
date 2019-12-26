using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Commands.UpdateToDoItem
{
    public class UpdateToDoItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand>
        {
            private readonly IToDoDbContext _context;

            public UpdateToDoItemCommandHandler(IToDoDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ToDoItem.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ToDoItem), request.Id);
                }

                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
