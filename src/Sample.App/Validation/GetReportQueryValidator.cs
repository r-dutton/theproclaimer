using FluentValidation;
using Sample.App.Requests;

namespace Sample.App.Validation;

public class GetReportQueryValidator : AbstractValidator<GetReportQuery>
{
    public GetReportQueryValidator()
    {
        RuleFor(x => x.ReportId).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
    }
}
