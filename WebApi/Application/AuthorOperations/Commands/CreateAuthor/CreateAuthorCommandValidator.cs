using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);
    }
}
