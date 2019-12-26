using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.ToDoItem.Queries
{
    public class GetToDoItemsQuery : IRequest<ToDoItemsViewModel>
    {
        public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, ToDoItemsViewModel>
        {
            private readonly IToDoDbContext _context;

            public GetToDoItemsQueryHandler(IToDoDbContext context)
            {
                _context = context;
            }

            public async Task<ToDoItemsViewModel> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken) =>
                new ToDoItemsViewModel
                {
                    ToDoItems = await _context.ToDoItem
                        .OrderBy(t => t.Name)
                        .Select(s => new ToDoItemViewModel().Map(s))
                        .ToListAsync(cancellationToken)
                };
        }
    }
}
