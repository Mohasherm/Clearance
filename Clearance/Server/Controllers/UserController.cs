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

        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var c = await (from u in db.Users
                           select new UserDTO
                           {
                               Id = u.Id,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Father = u.Father,
                               FullName = u.FirstName + " " + u.Father + " " + u.LastName,
                               DirectionName = u.Direction.Name,
                               Direction_Id = u.Direction_Id,
                               UserName = u.UserName
                           }).ToListAsync();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetUser/{Email}")]
        public async Task<ActionResult<UserDTO>> GetUser(string Email)
        {
            var c = await (from u in db.Users
                           where u.Email == Email
                           select new UserDTO
                           {
                               Id = u.Id,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Father = u.Father,
                               FullName = u.FirstName + " " + u.Father + " " + u.LastName,
                               DirectionName = u.Direction.Name,
                               Direction_Id = u.Direction_Id,
                               UserName = u.UserName
                           }).FirstOrDefaultAsync();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }
        [HttpGet]
        [Route("GetUserById/{Id:Guid}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid Id)
        {
            var c = await (from u in db.Users
                           where u.Id == Id
                           select new UserDTO
                           {
                               Id = u.Id,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Father = u.Father,
                               FullName = u.FirstName + " " + u.Father + " " + u.LastName,
                               DirectionName = u.Direction.Name,
                               Direction_Id = u.Direction_Id,
                               UserName = u.UserName
                           }).FirstOrDefaultAsync();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }


        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            IdentityResult result;

            AppUser newUser = new()
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Father = userRegisterDTO.Father,
                Direction_Id = userRegisterDTO.Direction_Id,
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

            await SeedRoles();
            result = await _userManager.AddToRoleAsync(newUser, "User");

            return CreatedAtAction(nameof(Register), new UserRegisterResultDTO { Succeeded = true });
        }

        private async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new AppRole("Admin"));

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new AppRole("User"));
        }

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


        [HttpGet("GetUserRoles/{RoleName:alpha}")]
        public async Task<ActionResult<List<UserDTO>>> GetUserRoles(string RoleName)
        {
            var Roles = await _userManager.GetUsersInRoleAsync(RoleName);
            if (Roles == null)
            {
                return NoContent();
            }
            var result = (from u in Roles
                          select new UserDTO
                          {
                              Id = u.Id,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Father = u.Father,
                              FullName = u.FirstName + " " + u.Father + " " + u.LastName,
                              DirectionName = u.Direction.Name,
                              Direction_Id = u.Direction_Id,
                              UserName = u.UserName
                          }).ToList();
            return Ok(result);
        }

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

        [HttpPut]
        [Route("PutUser/{Id:Guid}")]
        public async Task<ActionResult> PutUser([FromBody] UserDTO userDTO,Guid Id)
        {
            if (userDTO == null || userDTO.Id != Id)
                return NotFound();

            var data = await db.Users.FindAsync(userDTO.Id);
            data.FirstName = userDTO.FirstName;
            data.LastName = userDTO.LastName;
            data.Father = userDTO.Father;
            data.Direction_Id = userDTO.Direction_Id;

            db.Entry(data).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
                return Ok(data);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [HttpGet("Search/{Name}")]
        public async Task<ActionResult<List<UserDTO>>> Search(string Name)
        {
            var data =  await (
            from a in db.Users
            where a.FirstName.Contains(Name) || a.LastName.Contains(Name) 
            || a.Father.Contains(Name)|| a.UserName.Contains(Name)
            select new UserDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Father = a.Father,
                FullName = a.FirstName + " " + a.Father + " " + a.LastName,
                DirectionName = a.Direction.Name,
                Direction_Id = a.Direction_Id,
                UserName = a.UserName
            }
               ).ToListAsync();

            return Ok(data);
        }


        [HttpDelete("Delete/{Id:Guid}")]
        public async Task<ActionResult<bool>> Delete(Guid Id)
        {
            var data = await db.Users.FindAsync(Id);
            if (data == null)
            {
                return NotFound(false);
            }
            db.Remove(data);
            try
            {
                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
           
        }
    }
}
