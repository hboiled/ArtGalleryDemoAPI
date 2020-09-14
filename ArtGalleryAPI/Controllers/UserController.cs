using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ArtGalleryAPIUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ArtGalleryAPIUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.CuratorId);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                // TODO: Find way to add roles to user
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // configure
                var authSigningKey = new SymmetricSecurityKey((Encoding.UTF8.GetBytes("secretKeyTempDoNotUseInProductionPlease")));

                //var token = new JwtSecurityToken(
                //    //issuer: _configuration["JWT:ValidIssuer"],
                //    //audience: _configuration["JWT:ValidAudience"],
                //    expires: DateTime.UtcNow.AddDays(7),                    
                //    claims: authClaims,
                //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)                   
                //    );

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    //token = new JwtSecurityTokenHandler().WriteToken(token),
                    token = tokenString,
                    curatorId = user.UserName,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.CuratorId);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User already exists!" });
            }                

            ArtGalleryAPIUser user = new ArtGalleryAPIUser()
            {                
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.CuratorId
            };

            var userCreated = await userManager.CreateAsync(user, model.Password);
            var addedRole = await roleManager.CreateAsync(new IdentityRole(UserRoles.Curator));

            if (!userCreated.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error",
                        Message = "User creation failed! Please check user details and try again." });
            }
                

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

    }
}