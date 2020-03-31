namespace Timesheet.Core.Models.Helpers
{
    public class OperationQuery
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string Sort { get; set; }
    }
}
