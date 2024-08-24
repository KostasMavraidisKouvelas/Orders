using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Orders.DataAccess;
using Orders.DTO;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _usersManager;
        private readonly IConfiguration _configuration;

        public UserService(OrdersDbContext context, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _usersManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginDto user)
        {
            var existingUser = await _usersManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                throw new Exception("Invalid username or password.");
            }

            var result = await _usersManager.CheckPasswordAsync(existingUser, user.Password);

            if (!result)
            {
                throw new Exception("Invalid username or password.");
            }

            var secret = _configuration["JWTConfig:Secret"];
            var claims = await _usersManager.GetClaimsAsync(existingUser);
            var jwtToken = GenerateJwtToken(existingUser, secret, claims);

            return jwtToken;
        }

        public async Task<string> Register(RegisterUserDto user)
        {
            try
            {
                var existingUser = await _usersManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    throw new Exception("User already exists.");
                }

                var newUser = new User
                {
                    Email = user.Email,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                // Add claim "mobileUser" to the user
          

                var result = await _usersManager.CreateAsync(newUser, user.Password);
                await _usersManager.AddClaimAsync(newUser, new Claim("mobileUser", "true"));

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user.");
                }

                var secret = _configuration["JWTConfig:Secret"];
                var claims = await _usersManager.GetClaimsAsync(newUser);
                var jwtToken = GenerateJwtToken(newUser, secret, claims);

                return jwtToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user.", ex);
            }
        }


        private static string GenerateJwtToken(User user, string secret, IList<Claim> claims)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();


            claims.Add(new Claim("id", user.Id.ToString()));
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
