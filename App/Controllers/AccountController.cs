using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Authentication;
using App.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Authorize]

    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Route("{userId}/friends")]
        public async Task<IActionResult> GetFriends(string userId)
        {
            var contentType = this.Request;

            //var uid = await _userManager.GetUserAsync(User);
            //var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var u = User.Identity.;

            //if (UserId == userId)
            {
                return Ok(await _context.UserFriends.Where(x => x.UserId == userId).ToListAsync());
            }

            //return Unauthorized();
        }
    }
}