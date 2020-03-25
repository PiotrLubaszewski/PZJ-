using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Exceptions;
using Timesheet.Core.Extensions;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Accounts;
using Timesheet.Core.Models.Collections.Interfaces;
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
            if (!result.Succeeded) throw new InvalidValidationException(nameof(model.UserName).ToCamelCase(), $"'{nameof(model.UserName).ToPascalCase().InsertSpaces()}' or '{nameof(model.Password).ToPascalCase().InsertSpaces()}' is invalid.");

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

        public async Task<ICollectionResult<AccountModel>> GetUsersAsync(OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            return await _userManager.Users.ToCollectionResultAsync<ApplicationUser, AccountModel>(operationQuery, cancellationToken);
        }

        public async Task<ICollectionResult<RoleModel>> GetRolesAsync(OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.ToCollectionResultAsync<ApplicationRole, RoleModel>(operationQuery, cancellationToken);
        }

        public async Task<ICollectionResult<RoleModel>> GetUserRolesAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out _)) new InvalidValidationException(nameof(userId).ToCamelCase(), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            return await _roleManager.Roles.Where(x => roles.Contains(x.Name)).ToCollectionResultAsync<ApplicationRole, RoleModel>(operationQuery, cancellationToken);
        }

        public async Task<AccountModel> GetUserByIdAsync(string userId)
        {
            if (!Guid.TryParse(userId, out _)) new InvalidValidationException(nameof(userId).ToCamelCase(), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            return (await _userManager.FindByIdAsync(userId)).Adapt<AccountModel>();
        }

        public async Task AddAccountAsync(AddAccountModel model)
        {
            var userWithGivenEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithGivenEmail != null) throw new InvalidValidationException(nameof(model.Email).ToCamelCase(), $"'{nameof(model.Email).ToPascalCase().InsertSpaces()}' is taken.");

            var userWithGivenUsername = await _userManager.FindByNameAsync(model.UserName);
            if (userWithGivenUsername != null) throw new InvalidValidationException(nameof(model.UserName).ToCamelCase(), $"'{nameof(model.UserName).ToPascalCase().InsertSpaces()}' is taken.");

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true,
                CreatedDateTime = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) throw new InvalidOperationException("An error ocurred during creating new account.");
        }

        public async Task AddUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out _)) new InvalidValidationException(nameof(userId).ToCamelCase(), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any()) throw new InvalidOperationException("This account is already in role(s).");

            var rolesNames = await _roleManager.Roles.Where(x => model.RolesIds.Select(id => Guid.Parse(id)).Contains(x.Id)).Select(x => x.Name).ToListAsync(cancellationToken);
            var result = await _userManager.AddToRolesAsync(user, rolesNames);
            if (!result.Succeeded) throw new InvalidOperationException("An error ocurred during adding roles.");
        }

        public async Task UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out _)) new InvalidValidationException(nameof(userId).ToCamelCase(), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any()) throw new InvalidOperationException("This account has no role(s).");

            var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!removeResult.Succeeded) throw new InvalidOperationException("An error ocurred during adding roles.");

            var rolesNames = await _roleManager.Roles.Where(x => model.RolesIds.Select(id => Guid.Parse(id)).Contains(x.Id)).Select(x => x.Name).ToListAsync(cancellationToken);
            var result = await _userManager.AddToRolesAsync(user, rolesNames);
            if (!result.Succeeded) throw new InvalidOperationException("An error ocurred during adding roles.");
        }
    }
}
