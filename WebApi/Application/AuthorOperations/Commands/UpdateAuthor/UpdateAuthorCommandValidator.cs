using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).GreaterThan(0);
        RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(4);
    }
}
