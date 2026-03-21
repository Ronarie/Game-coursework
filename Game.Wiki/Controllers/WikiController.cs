using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Game.Wiki.DTOs;
using Game.Wiki.Services;
using System.Security.Claims;

namespace Game.Quest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WikiController : ControllerBase
    {
        private readonly WikiService _wikiService;

        public WikiController(WikiService wikiService)
        {
            _wikiService = wikiService;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _wikiService.GetAllLocations();
            return Ok(locations);
        }

        [HttpGet("location/{uid}")]
        public async Task<IActionResult> GetByLocation(string name)
        {
            var location = await _wikiService.GetByLocation(name);
            if (location == null)
                return NotFound("Предмет не найден.");
            return Ok(location);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _wikiService.GetAllTypes();
            return Ok(types);
        }

        [HttpGet("type/{uid}")]
        public async Task<IActionResult> GetByType(string name)
        {
            var type = await _wikiService.GetByType(name);
            if (type == null)
                return NotFound("Предмет не найден.");
            return Ok(type);
        }
    }
}
