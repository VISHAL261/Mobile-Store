using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobileStore.Data;
using MobileStore.Dto;
using MobileStore.Interfaces;
using MobileStore.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly MobileStoreContext _context;


        public AuthenticateController(MobileStoreContext context, IConfiguration configuration, IJWTManagerRepository jWTManager)
        {
            _configuration = configuration;
            _jWTManager = jWTManager;
            _context = context;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("User")]
        public IActionResult Authenticate(LoginDto usersdata)
        {
            if (!MobileInformationExists(usersdata.Email))
            {
                return NotFound();
            }
            
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        private bool MobileInformationExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }

    }
}