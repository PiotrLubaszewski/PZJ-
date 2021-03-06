﻿namespace Timesheet.Persistence.Entities.Identities
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class ApplicationRole : IdentityRole<Guid>, IEntity<Guid>
    {
        public DateTime CreatedDateTime { get; set; }
    }
}
