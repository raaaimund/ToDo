using FluentValidation;

namespace ToDo.Application.ToDoItem.Commands.UpdateToDoItem
{
    public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
    {
        public UpdateToDoItemCommandValidator()
        {
            RuleFor(p => p.Name).MaximumLength(128).NotEmpty();
        }
    }
}
