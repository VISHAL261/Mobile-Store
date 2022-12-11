using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStore.Data;
using MobileStore.Dto;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MobileStoreContext _context;
        private readonly ILogger<MobileStoreController> _logger;

        public UserController(MobileStoreContext context, ILogger<MobileStoreController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> PostUser(UserDto dto)
        {

            if (!UserExists(dto.Email))
            {
                var user = new User();
                user.UserName = dto.UserName;
                user.Email = dto.Email;
                user.Password = dto.Password;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                return UnprocessableEntity();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private bool UserExists(string Email)
        {
            return _context.User.Any(e => e.Email == Email);
        }
    }
}
