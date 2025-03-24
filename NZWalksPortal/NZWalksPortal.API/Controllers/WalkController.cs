using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Models.DTO;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IRegionRepository regionRepository,
            IDifficultyRepository difficultyRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto request)
        {
            var walkDomain = mapper.Map<Walk>(request);
            walkDomain = await walkRepository.AddWalkAsync(walkDomain);
            if(walkDomain.RegionId != Guid.Empty)
            {
                var existingRegion = await regionRepository.GetRegionByIdAsync(walkDomain.RegionId);
                if (existingRegion is not null)
                    walkDomain.Region = existingRegion;
            }
            if (walkDomain.DifficultyId != Guid.Empty)
            {
                var existingDifficulty = await difficultyRepository.GetDifficultyByIdAsync(walkDomain.DifficultyId);
                if (existingDifficulty is not null)
                    walkDomain.Difficulty = existingDifficulty;
            }
                var responseDto = mapper.Map<WalkDto>(walkDomain);
            return CreatedAtAction(nameof(GetWalkById), new { id = responseDto.Id }, responseDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDomain = await walkRepository.GetAllWalksAsync();
            if (walkDomain is null)
                return NotFound();
            foreach (var walk in walkDomain)
            {
                if (walk.RegionId != Guid.Empty)
                {
                    var existingRegion = await regionRepository.GetRegionByIdAsync(walk.RegionId);
                    if (existingRegion is not null)
                        walk.Region = existingRegion;
                }
                if (walk.DifficultyId != Guid.Empty)
                {
                    var existingDifficulty = await difficultyRepository.GetDifficultyByIdAsync(walk.DifficultyId);
                    if (existingDifficulty is not null)
                        walk.Difficulty = existingDifficulty;
                }
            }
            return Ok(mapper.Map<IEnumerable<WalkDto>>(walkDomain));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomain is null)
                return NotFound();
            if (walkDomain.RegionId != Guid.Empty)
            {
                var existingRegion = await regionRepository.GetRegionByIdAsync(walkDomain.RegionId);
                if (existingRegion is not null)
                    walkDomain.Region = existingRegion;
            }
            if (walkDomain.DifficultyId != Guid.Empty)
            {
                var existingDifficulty = await difficultyRepository.GetDifficultyByIdAsync(walkDomain.DifficultyId);
                if (existingDifficulty is not null)
                    walkDomain.Difficulty = existingDifficulty;
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteWalkByIdAsync(id);
            if (walkDomain is null)
                return NotFound();
            if (walkDomain.RegionId != Guid.Empty)
            {
                var existingRegion = await regionRepository.GetRegionByIdAsync(walkDomain.RegionId);
                if(existingRegion is not null)
                    walkDomain.Region = existingRegion;
            }
            if (walkDomain.DifficultyId != Guid.Empty)
            {
                var existingDifficulty = await difficultyRepository.GetDifficultyByIdAsync(walkDomain.DifficultyId);
                if(existingDifficulty is not null)
                    walkDomain.Difficulty = existingDifficulty;
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}
