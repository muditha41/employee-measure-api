using System;
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
        [HttpPut]
        [Route("updatedetails")]
        public async Task<IActionResult> UpdateDetails([FromBody] UserResource userUpdateRecource)
        {
            var userExists= _context.Users.Where(i => i.Id == userUpdateRecource.Id).SingleOrDefault();
            if (userExists != null)
            {
            
                userExists.UserName = userUpdateRecource.UserName;
                userExists.FullName = userUpdateRecource.FullName;
                userExists.Email = userUpdateRecource.Email;
                userExists.Description = userUpdateRecource.Description;
                userExists.Location = userUpdateRecource.Location;
                userExists.WorkPlace = userUpdateRecource.WorkPlace;
                userExists.RelationshipStatus = userUpdateRecource.RelationshipStatus;
               
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok(new Response { Status = "Success", Message = "User Details Successfully Updated!" });

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
            
        }




        }
}