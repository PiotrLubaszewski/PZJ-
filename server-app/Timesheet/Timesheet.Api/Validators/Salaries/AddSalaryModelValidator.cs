using FluentValidation;
using Timesheet.Core.Extensions;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Api.Validators.Salaries
{
    public class AddSalaryModelValidator : AbstractValidator<AddSalaryModel>
    {
        private const decimal _minimalAmountValue = 1000.0m;
        private const decimal _maximalAmountValue = 9999999.0m;
        private const decimal _minimalTimeRate = 50.0m;
        private const decimal _minimalRate = 0.0m;
        private const decimal _maximalRate = 100.0m;

        public AddSalaryModelValidator()
        {
            RuleFor(x => x.StartDateTime).Must(x => x.Day == 1).WithMessage(x => $"'{nameof(x.StartDateTime).InsertSpaces()}' must contain date with 1st day of month.");
            RuleFor(x => x.Amount).InclusiveBetween(_minimalAmountValue, _maximalAmountValue);
            RuleFor(x => x.MinimumTimeRate).InclusiveBetween(_minimalTimeRate, _maximalRate);
            RuleFor(x => x.OvertimeRate).InclusiveBetween(_minimalRate, _maximalRate);
            RuleFor(x => x.FineRate).InclusiveBetween(_minimalRate, _maximalRate);
        }
    }
}
