using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheet.Api.Models.Settings;
using Timesheet.Core.Models.Helpers;

namespace Timesheet.Api.Infrastructure.Utils
{
    public static class JwtUtils
    {
        public static string CreateJwtTokenFromJwtTokenModel(JwtTokenModel model, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim("userId", model.UserId),
                new Claim("userName", model.Username),
                new Claim("firstName", model.FirstName),
                new Claim("lastName", model.LastName)
            };

            foreach (var role in model.Roles)
            {
                claims.Add(new Claim("roles", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.ExpireDays));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
