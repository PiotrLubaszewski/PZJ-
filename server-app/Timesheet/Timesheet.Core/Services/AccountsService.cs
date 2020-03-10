using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Timesheet.Core.Exceptions;
using Timesheet.Core.Extensions;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Queries.Accounts;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountsService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<JwtTokenModel> GetJwtTokenModelAsync(AuthorizeQuery model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded) throw new InvalidValidationException(nameof(model.UserName).ToCamelCase(), $"Given {nameof(model.UserName)} or {nameof(model.Password)} is invalid.");

            var user = await _userManager.FindByNameAsync(model.UserName);
            var roles = await _userManager.GetRolesAsync(user);

            return new JwtTokenModel
            {
                UserId = user.Id.ToString(),
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };
        }
    }
}
