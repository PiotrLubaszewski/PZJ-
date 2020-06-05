using FluentValidation;
using Timesheet.Core.Models.WorkedHours;

namespace Timesheet.Api.Validators.WorkedHours
{
    public class AddWorkedHourModelValidator : AbstractValidator<AddWorkedHourModel>
    {
        public AddWorkedHourModelValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0);
            RuleFor(x => x.ProjectTaskId).GreaterThan(0);
            RuleFor(x => x.HoursQuantity).GreaterThan(0);
        }
    }
}
