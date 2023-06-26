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
        public async Task<ActionResult<List<ClearanceDTO>>> Search(Guid Id, string Name)
        {
            var c = await clearanceService.Search(Id, Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("SearchbyDirection/{Id:Guid}/{Name}")]
        public async Task<ActionResult<List<ClearanceDirectionsDTO>>> SearchbyDirection(Guid Id, string Name)
        {
            var c = await clearanceService.SearchbyDirection(Id, Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("SearchbyDirectionState/{Id:Guid}/{Name}/{state:bool}")]
        public async Task<ActionResult<List<ClearanceDirectionsDTO>>> SearchbyDirectionState(Guid Id, string Name,bool state)
        {
            var c = await clearanceService.SearchbyDirectionState(Id, Name,state);
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

        [HttpGet]
        [Route("GetAllByDirection/{Id:Guid}")]
        public async Task<ActionResult<List<ClearanceDirectionsDTO>>> GetAllByDirection(Guid Id)
        {
            var c = await clearanceService.GetAllByDirection(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetByclId/{Id}")]
        public async Task<ActionResult<List<ClearanceDirectionsDTO>>> GetByclId(int Id)
        {
            var c = await clearanceService.GetByclId(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetAllByDirectionState/{Id:Guid}/{state:bool}")]
        public async Task<ActionResult<List<ClearanceDirectionsDTO>>> GetAllByDirectionState(Guid Id,bool state)
        {
            var c = await clearanceService.GetAllByDirectionState(Id,state);
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

        [HttpGet("GetByDirectionId/{Id}")]
        public async Task<ActionResult<ClearanceDirectionsDTO>> GetByDirectionId(int Id)
        {
            var c = await clearanceService.GetByDirectionId(Id);
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

        [HttpPut]
        [Authorize(Roles = "Admin,SuperVisor")]
        [Route("PutDirection/{Id}")]
        public async Task<ActionResult> PutDirection([FromBody] ClearanceDirectionsDTO clearanceDirectionsDTO, int Id)
        {
            var data = await clearanceService.UpdateDirection( clearanceDirectionsDTO, Id);
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
