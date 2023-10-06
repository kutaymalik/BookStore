using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.FirstName).MinimumLength(3).When(x=> x.Model.FirstName != string.Empty);
        RuleFor(command => command.Model.LastName).MinimumLength(3).When(x=> x.Model.LastName != string.Empty);
    }
}
