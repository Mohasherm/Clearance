using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Clearance.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollageController : ControllerBase
    {
        private readonly ICollageService collageService;

        public CollageController(ICollageService collageService)
        {
            this.collageService = collageService;
        }

        [HttpGet]
        [Route("Search/{Name}")]
        public async Task<ActionResult<List<CollageDTO>>> Search(string Name)
        {
            var c = await collageService.Search(Name);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<CollageDTO>>> GetAll()
        {
            var c = await collageService.GetAll();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpGet("GetById/{Id}")]

        public async Task<ActionResult<CollageDTO>> GetByID(int Id)
        {
            var c = await collageService.GetById(Id);
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] CollageDTO collageDTO)
        {
            var data = await collageService.Insert(collageDTO);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("Put/{Id}")]
        public async Task<ActionResult> Put([FromBody] CollageDTO collageDTO, int Id)
        {
            var data = await collageService.Update(collageDTO, Id);
            if (data)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var result = await collageService.Delete(Id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
