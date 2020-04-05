using System;

namespace Timesheet.Persistence.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
