using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;

        public DifficultyController(IDifficultyRepository difficultyRepository)
        {
            this.difficultyRepository = difficultyRepository;
        }

        [HttpGet("{id}/GetDifficultyById")]
        public async Task<IActionResult> GetDifficultyById([FromRoute] Guid id)
        {
            var difficultyDomain = await difficultyRepository.GetDifficultyByIdAsync(id);
            if (difficultyDomain is null)
                return NotFound();
            return Ok(difficultyDomain);
        }
    }
}
