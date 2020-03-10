using System.Collections.Generic;
using Timesheet.Core.Models.Collections.Interfaces;

namespace Timesheet.Core.Models.Collections
{
    public class CollectionResult<T> : ICollectionResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
