using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clearance.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollageDirectionController : ControllerBase
    {
        private readonly IDepartmentDirectionService departmentDirectionService;

        public CollageDirectionController(IDepartmentDirectionService departmentDirectionService)
        {
            this.departmentDirectionService = departmentDirectionService;
        }

        [HttpGet]
        [Route("GetAll/{Id}")]
        public async Task<ActionResult<List<DepartmentDirectionDTO>>> GetAll(int Id)
        {
            var c = await departmentDirectionService.GetAll(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet("GetById/{Id}")]

        public async Task<ActionResult<DepartmentDirectionDTO>> GetByID(int Id)
        {
            var c = await departmentDirectionService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] DepartmentDirectionDTO departmentDirectionDTO)
        {
            var data = await departmentDirectionService.Insert(departmentDirectionDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await departmentDirectionService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
