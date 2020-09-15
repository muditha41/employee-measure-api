﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using App.Authentication;
using App.Controllers.Resources;
using App.DB;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
  //  [Authorize]

    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(ApplicationIdentityDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}/friends")]
        public async Task<IActionResult> GetFriends(string userId)
        {
          //  var contentType = this.Request;

           var userExists = await _userManager.FindByIdAsync(userId);
           if (userExists != null) {
                var userFriends = await _context.UserFriends.Include(s => s.User).Include(r => r.Friend).Where(x => x.UserId == userId && x.InviteStatus==true).ToListAsync();
                var userFriendsResource = _mapper.Map<List<UserFriendResource>>(userFriends);
                if (userFriendsResource.Count>0)
                {
                    return Ok(userFriendsResource);

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User doesn't have friends!" });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User doesn't exists!" });
            }
             //return Unauthorized();
        }

        [HttpPost]
        [Route("invite")]
        public async Task<IActionResult> PostInvitation([FromBody] InvitationResouce invitation)
        {
            var friend = await _userManager.FindByEmailAsync(invitation.Email);
            
            if (friend != null)
            {
                var friendExist = await _context.UserFriends.Where(x => x.UserId == invitation.UserId && x.FriendId== friend.Id ).ToListAsync();
                if (friendExist.Count==0) {
                    UserFriends userFriend = new UserFriends()
                    {
                        UserId = invitation.UserId,
                        FriendId = friend.Id,
                        InviteStatus = false,


                    };
                    var result = await _context.UserFriends.AddAsync(userFriend);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok(new Response { Status = "Success", Message = "Invitation Successful!" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invitation Failed" });
                    }

                } else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Already Friend" });
                }
               
                   
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invalid Email or User doesn't Exists" });
            }
           //     var contentType = this.Request;

           
        }

        [HttpPut]
        [Route("acceptinvite")]
        public async Task<IActionResult> PostAcceptInvitation([FromBody] InvitationResouce invitation)
        {
            var friend = await _userManager.FindByEmailAsync(invitation.Email);

            if (friend != null)
            {
                var friendExist = await _context.UserFriends.Where(x => x.UserId == invitation.UserId && x.FriendId == friend.Id).SingleOrDefaultAsync();
                if (friendExist !=null)
                {
                    if (friendExist?.InviteStatus == false)
                    {
                        friendExist.InviteStatus = true;
                        //  var result = await _context.UserFriends.Update(friendExist);
                        UserFriends userFriend = new UserFriends()
                        {
                            UserId = friend.Id ,
                            FriendId = invitation.UserId,
                            InviteStatus = true,


                        };
                        var result = await _context.UserFriends.AddAsync(userFriend);

                       
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            return Ok(new Response { Status = "Success", Message = "Friend Request Accepted!" });
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Friend Request Failed" });
                        }

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Friend Request Already Accepted" });
                    }
                  
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invalid Friend Requests" });
                }


            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invalid Email or User doesn't Exists" });
            }
            //     var contentType = this.Request;


        }

        [HttpGet]
        [Route("{userId}/friendrequests")]
        public async Task<IActionResult> GetFriendsRequest(string userId)
        {
            //  var contentType = this.Request;

            var userExists = await _userManager.FindByIdAsync(userId);
            if (userExists != null)
            {
                var userFriends = await _context.UserFriends.Include(s => s.User).Where(x => x.FriendId == userId && x.InviteStatus == false).ToListAsync();
                var userFriendsResource = _mapper.Map<List<UserFriendResource>>(userFriends);
                if (userFriendsResource.Count > 0)
                {
                    return Ok(userFriendsResource);

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User doesn't have friends!" });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User doesn't exists!" });
            }
            //return Unauthorized();
        }
    }
}