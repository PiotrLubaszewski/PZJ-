using System.Collections.Generic;

namespace Timesheet.Core.Models.Collections.Interfaces
{
    public interface ICollectionResult<T> where T : class
    {
        IEnumerable<T> Data { get; set; }
        int TotalCount { get; set; }
        int PagesCount { get; set; }
        int CurrentPage { get; set; }
    }
}
