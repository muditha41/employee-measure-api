using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using App.Authentication;
using App.Controllers.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    //  [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public StatusController(ApplicationIdentityDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }


        [HttpPut]
        [Route("statusupdate")]
        public async Task<IActionResult> PostStatus([FromBody]  UserStatusResource userStatusRecource)
        {
            var userStatusExists = await _context.UserStatus.Where(x => x.UserStatusId == userStatusRecource.UserStatusId).SingleOrDefaultAsync();
            if (userStatusExists != null)
            {
             
                //getting userFrien from friend-end
                var userFrindRevert = await _context.UserFriends.Include(s => s.UserSatus).Where(
                     x=>x.UserId == userStatusRecource.FriendId 
                     && x.FriendId== userStatusRecource.UserId 
                     && x.InviteStatus==true)
                       .SingleOrDefaultAsync();
                if (userFrindRevert != null)
                {
                    //updating user status
                    var dateTimeNow = DateTime.Now;
                    userStatusExists.StatusId = userStatusRecource.StatusId;
                    userStatusExists.StatusTimeStamp = dateTimeNow;

                    //updating user status on frind-end
                    userFrindRevert.UserSatus.FriendStatusId = userStatusRecource.StatusId;
                    userFrindRevert.UserSatus.FriendStatusTimeStamp = dateTimeNow;
                }

                    if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok(new Response { Status = "Success", Message = "Status Updated!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Status Update Failed" });
                }

            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Not Found" });
            }

          
        }
    }
}
