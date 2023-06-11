using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Clearance.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IClaimsService _claimsService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly DataContext db;
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IClaimsService claimsService, IJwtTokenService jwtTokenService, DataContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsService = claimsService;
            _jwtTokenService = jwtTokenService;
            this.db = db;
        }

        //[HttpGet]
        //[Route("GetUsers")]
        //public async Task<ActionResult<List<UserDTO>>> GetUsers()
        //{
        //    var c = await (from u in db.Users
        //                   select new UserDTO
        //                   {
        //                       Id = u.Id,
        //                       Name = u.Name,
        //                       UserName = u.UserName
        //                   }).ToListAsync();
        //    if (c == null)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(c);
        //}

        //[HttpGet]
        //[Route("GetUser/{Email}")]
        //public async Task<ActionResult<UserDTO>> GetUser(string Email)
        //{
        //    var c = await (from u in db.Users
        //                   where u.Email == Email
        //                   select new UserDTO
        //                   {
        //                       Id = u.Id,
        //                       Name = u.Name,
        //                       UserName = u.UserName
        //                   }).FirstOrDefaultAsync();
        //    if (c == null)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(c);
        //}


        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            IdentityResult result;

            AppUser newUser = new()
            {
                Email = userRegisterDTO.Email,
                UserName = userRegisterDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            result = await _userManager.CreateAsync(newUser, userRegisterDTO.Password);

            if (!result.Succeeded)
                return Conflict(new UserRegisterResultDTO
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(e => e.Description)
                });

            //await SeedRoles();
            //result = await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return CreatedAtAction(nameof(Register), new UserRegisterResultDTO { Succeeded = true });
        }

        //private async Task SeedRoles()
        //{
        //    if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.SuperAdmin));

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.MaintenanceAdmin))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.MaintenanceAdmin));

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.QulityAdmin))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.QulityAdmin));

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.BuyAdmin))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.BuyAdmin));

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.SuperVisor))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.SuperVisor));

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //        await _roleManager.CreateAsync(new AppRole(UserRoles.User));
        //}

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                var userClaims = await _claimsService.GetUserClaimsAsync(user);

                var token = _jwtTokenService.GetJwtToken(userClaims);

                return Ok(new UserLoginResultDTO
                {
                    Succeeded = true,
                    Token = new TokenDTO
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    }
                });
            }

            return Unauthorized(new UserLoginResultDTO
            {
                Succeeded = false,
                Message = "The email and password combination was invalid."
            });
        }

        [HttpPost]
        [Route("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDTO.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, changePasswordDTO.CurrentPassword))
            {
                await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
                return Ok();
            }
            else
            {
                return BadRequest("current password not match with current user");
            }
        }

        [HttpGet("GetRoleForUser/{Id:Guid}")]
        public async Task<ActionResult<IList<string>>> GetRoleForUser(Guid Id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.GetRolesAsync(user);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        //[HttpGet("GetUserRoles/{RoleName:alpha}")]
        //public async Task<ActionResult<List<UserDTO>>> GetUserRoles(string RoleName)
        //{
        //    var Roles = await _userManager.GetUsersInRoleAsync(RoleName);
        //    if (Roles == null)
        //    {
        //        return NoContent();
        //    }
        //    var result = (from u in Roles
        //                  select new UserDTO
        //                  {
        //                      Id = u.Id,
        //                      Name = u.Name,
        //                      UserName = u.UserName
        //                  }).ToList();
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("SetUserRole")]
        public async Task<IActionResult> SetUserRole([FromBody] UserRolesDTO userRolesDTO)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userRolesDTO.User_Id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, userRolesDTO.RoleName);

            return Ok();
        }
    }
}
