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
        private readonly ICollageDirectionService collageDirectionService;

        public CollageDirectionController(ICollageDirectionService collageDirectionService)
        {
            this.collageDirectionService = collageDirectionService;
        }

        [HttpGet]
        [Route("GetAll/{Id}")]
        public async Task<ActionResult<List<CollageDirectionDTO>>> GetAll(int Id)
        {
            var c = await collageDirectionService.GetAll(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet("GetById/{Id}")]

        public async Task<ActionResult<CollageDirectionDTO>> GetByID(int Id)
        {
            var c = await collageDirectionService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] CollageDirectionDTO collageDirectionDTO)
        {
            var data = await collageDirectionService.Insert(collageDirectionDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await collageDirectionService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
