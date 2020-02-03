using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PricingEngine.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PricingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PricingEngineContext _context;
        private readonly JWTSettings _jwtsettings;

        public UsersController(PricingEngineContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] AppUser user)
         {
            try
            {
                user = await _context.AppUser
                                .Where(u => u.EmailId == user.EmailId
                                    && u.Password == user.Password)
                                .FirstOrDefaultAsync();

                UserWithToken userWithToken = null;

                if (user != null)
                    userWithToken = new UserWithToken(user);

                if (userWithToken == null)
                {
                    return NotFound();
                }

                // sign your token here here..
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.EmailId)
                    }),
                    Expires = DateTime.UtcNow.AddMonths(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                userWithToken.Token = tokenHandler.WriteToken(token);

                return userWithToken;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
