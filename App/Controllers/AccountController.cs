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

        

       
        

        }
}