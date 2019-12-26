using FluentValidation;

namespace ToDo.Application.ToDoItem.Commands.CreateToDoItem
{
    public class CreateToDoItemValidator : AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemValidator()
        {
            RuleFor(p => p.Name).MaximumLength(128).NotEmpty();
        }
    }
}
