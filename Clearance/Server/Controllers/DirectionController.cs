using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clearance.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionService directionService;

        public DirectionController(IDirectionService directionService)
        {
            this.directionService = directionService;
        }

        [HttpGet]
        [HttpGet("Search/{Name}")]
        public async Task<ActionResult<List<DirectionDTO>>> Search(string Name)
        {
            var c = await directionService.Search(Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<DirectionDTO>>> GetAll()
        {
            var c = await directionService.GetAll();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet("GetById/{Id}")]

        public async Task<ActionResult<DirectionDTO>> GetByID(int Id)
        {
            var c = await directionService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] DirectionDTO directionDTO)
        {
            var data = await directionService.Insert(directionDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("Put/{Id}")]
        public async Task<ActionResult> Put([FromBody] DirectionDTO directionDTO, int Id)
        {
            var data = await directionService.Update(directionDTO, Id);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await directionService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
