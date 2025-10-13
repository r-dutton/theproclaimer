using FluentValidation;
using Sample.App.Dtos;

namespace Sample.App.Validation;

public class ReportDtoValidator : AbstractValidator<ReportDto>
{
    public ReportDtoValidator()
    {
        RuleFor(r => r.Title).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Body).NotEmpty();
        RuleFor(r => r.Summary).NotEmpty().MaximumLength(512);
        RuleFor(r => r.Classification).Must(c => c is "public" or "confidential");
    }
}
