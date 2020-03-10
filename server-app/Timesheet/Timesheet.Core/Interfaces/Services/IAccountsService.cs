using System.Threading.Tasks;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Queries.Accounts;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<JwtTokenModel> GetJwtTokenModelAsync(AuthorizeQuery model);
    }
}
