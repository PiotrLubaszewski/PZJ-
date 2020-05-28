using FluentValidation;
using Timesheet.Core.Models.ProjectTasks;

namespace Timesheet.Api.Validators.Projects
{
    public class UpdateProjectTaskModelValidator : AbstractValidator<UpdateProjectTaskModel>
    {
        public UpdateProjectTaskModelValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
        }
    }
}
