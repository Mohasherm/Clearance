using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clearance.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClearanceController : ControllerBase
    {
        private readonly IClearanceService clearanceService;

        public ClearanceController(IClearanceService clearanceService)
        {
            this.clearanceService = clearanceService;
        }

        [HttpGet]
        [Route("Search/{Name}")]
        public async Task<ActionResult<List<ClearanceDTO>>> Search(string Name)
        {
            var c = await clearanceService.Search(Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("Search/{Id:Guid}/{Name}")]
        public async Task<ActionResult<List<ClearanceDTO>>> Search(Guid Id,string Name)
        {
            var c = await clearanceService.Search(Id,Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        public async Task<ActionResult<List<ClearanceDTO>>> GetAll()
        {
            var c = await clearanceService.GetAll();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetAllByState/{state}")]
        public async Task<ActionResult<List<ClearanceDTO>>> GetAllByState(string State)
        {
            var c = await clearanceService.GetAllByState(State);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }


        [HttpGet]
        [Route("GetAllByState/{state}/{Name}")]
        public async Task<ActionResult<List<ClearanceDTO>>> GetAllByState(string State,string Name)
        {
            var c = await clearanceService.GetAllByState(State,Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetAllByUserId/{Id:Guid}")]
        public async Task<ActionResult<List<ClearanceDTO>>> GetAllByUserId(Guid Id)
        {
            var c = await clearanceService.GetAllByUserId(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

    
        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<ClearanceDTO>> GetByID(int Id)
        {
            var c = await clearanceService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] ClearanceDTO clearanceDTO)
        {
            var data = await clearanceService.Insert(clearanceDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperVisor")]
        [Route("Put/{Id}")]
        public async Task<ActionResult> Put([FromBody] ClearanceDTO clearanceDTO, int Id)
        {
            var data = await clearanceService.Update(clearanceDTO, Id);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await clearanceService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
