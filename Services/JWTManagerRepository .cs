using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobileStore.Data;
using MobileStore.Dto;
using MobileStore.Interfaces;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileStore.Services
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
        private readonly IConfiguration iconfiguration;

		public JWTManagerRepository( IConfiguration iconfiguration)
		{
			this.iconfiguration = iconfiguration;

		}
		public  Tokens Authenticate(LoginDto user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Email, user.Email)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { AccessToken = tokenHandler.WriteToken(token) };

		}
	}
}
