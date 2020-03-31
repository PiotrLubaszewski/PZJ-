using System.Collections.Generic;

namespace Timesheet.Core.Models.Accounts
{
    public class AddOrUpdateUserRolesModel
    {
        public IEnumerable<string> RolesIds { get; set; }
    }
}
