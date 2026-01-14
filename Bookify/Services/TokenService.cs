using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bookify.Models; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.Extensions.Configuration; 
using Microsoft.IdentityModel.Tokens; 

namespace Bookify.Services 
{
    public class TokenService : ITokenService 
    {
        private readonly IConfiguration _config; 
        private readonly UserManager<ApplicationUser> _userManager; 

        public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager) 
        {
            _config = config; 
            _userManager = userManager;
        } 

        public async Task<string> CreateTokenAsync(ApplicationUser user) 
        {
            var jwtSection = _config.GetSection("Jwt"); 
            var key = jwtSection.GetValue<string>("Key"); 
            var issuer = jwtSection.GetValue<string>("Issuer"); 
            var audience = jwtSection.GetValue<string>("Audience"); 
            var duration = jwtSection.GetValue<int>("DurationInMinutes"); 

            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""), 
                new Claim(ClaimTypes.NameIdentifier, user.Id), 
                new Claim(ClaimTypes.Name, user.UserName ?? "") 
            }; 

            // add role claims
            var roles = await _userManager.GetRolesAsync(user); 
            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            } 

            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)); 
            var creds = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256); 

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: creds
            ); 

            return new JwtSecurityTokenHandler().WriteToken(token); 
        } 
    } 
}