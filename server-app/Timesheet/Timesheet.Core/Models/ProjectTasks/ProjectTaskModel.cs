namespace Timesheet.Core.Models.ProjectTasks
{
    public class ProjectTaskModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
