using FluentValidation;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Api.Validators.Salaries
{
    public class UpdateSalaryModelValidator : AbstractValidator<UpdateSalaryModel>
    {
        private const decimal _minimalAmountValue = 1000.0m;
        private const decimal _maximalAmountValue = 9999999.0m;
        private const decimal _minimalTimeRate = 50.0m;
        private const decimal _minimalRate = 0.0m;
        private const decimal _maximalRate = 100.0m;

        public UpdateSalaryModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Amount).InclusiveBetween(_minimalAmountValue, _maximalAmountValue);
            RuleFor(x => x.MinimalTimeRate).InclusiveBetween(_minimalTimeRate, _maximalRate);
            RuleFor(x => x.OvertimeRate).InclusiveBetween(_minimalRate, _maximalRate);
            RuleFor(x => x.FineRate).InclusiveBetween(_minimalRate, _maximalRate);
        }
    }
}
