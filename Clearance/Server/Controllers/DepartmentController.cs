using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Clearance.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        [Route("Search/{Name}")]
        public async Task<ActionResult<List<DepartmentDTO>>> Search(string Name)
        {
            var c = await departmentService.Search(Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetAll/{Id}")]
        public async Task<ActionResult<List<DepartmentDTO>>> GetAll(int Id)
        {
            var c = await departmentService.GetAll(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<DepartmentDTO>> GetByID(int Id)
        {
            var c = await departmentService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] DepartmentDTO departmentDTO)
        {
            var data = await departmentService.Insert(departmentDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("Put/{Id}")]
        public async Task<ActionResult> Put([FromBody] DepartmentDTO departmentDTO, int Id)
        {
            var data = await departmentService.Update(departmentDTO, Id);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await departmentService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
