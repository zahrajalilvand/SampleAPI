using APISample.Models;
using APISample.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APISample.Controllers
{
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		IConfiguration configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("authenticate")]
		public ActionResult<string> Authenticate(AuthenticateRequestBody authenticateRequestBody)
		{
			var user = ValidateUserCredential(authenticateRequestBody.UserName, authenticateRequestBody.Password);
			if (user == null) { return Unauthorized(); }

			var securityKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));

			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claimsForToken = new List<Claim>();
			claimsForToken.Add(new Claim("UserId", user.UserId.ToString()));
			claimsForToken.Add(new Claim("FirstName", user.FirstName.ToString()));
			claimsForToken.Add(new Claim("LastName", user.LastName.ToString()));

			var jwtSecurityToken = new JwtSecurityToken(
				configuration["Authentication:Issuer"],
				configuration["Authentication:Audience"],
				claimsForToken,
				DateTime.UtcNow,
				DateTime.UtcNow.AddHours(1),
				signingCredentials);

			var token = new JwtSecurityTokenHandler()
				.WriteToken(jwtSecurityToken);

			return Ok(token);
		}

		private CityInfoUser ValidateUserCredential(string? userName, string? password)
		{
			return new CityInfoUser(1,
				userName ?? "",
				"Ali",
				"Alizadeh",
				"Tehran");
		}
	}
}
