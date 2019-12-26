using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Queries
{
    public class GetToDoItemQuery : IRequest<ToDoItemViewModel>
    {
        public Guid Id { get; set; }

        public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, ToDoItemViewModel>
        {
            private readonly IToDoDbContext _context;

            public GetToDoItemQueryHandler(IToDoDbContext context)
            {
                _context = context;
            }

            public async Task<ToDoItemViewModel> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
            {
                var item = await _context.ToDoItem.FindAsync(request.Id) 
                    ?? throw new NotFoundException(nameof(Domain.Entities.ToDoItem), request.Id);
                return new ToDoItemViewModel().Map(item);
            }
        }
    }
}
