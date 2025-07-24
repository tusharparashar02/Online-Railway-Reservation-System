using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

    public class JwtToken
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public JwtToken(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var userRole = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT Key is missing in configuration.");
            }

            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

    }