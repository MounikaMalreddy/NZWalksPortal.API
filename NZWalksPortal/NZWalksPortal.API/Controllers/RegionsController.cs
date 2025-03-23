using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Models.DTO;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto request)
        {
            var regionDomain = mapper.Map<Region>(request);
            regionDomain= await regionRepository.AddRegionAsync(regionDomain);
            var responseDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction(nameof(GetRegionById), new {id=responseDto.Id }, responseDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionDomain = await regionRepository.GetAllRegionsAsync();
            if(regionDomain is null)
                return NotFound();
            return Ok(mapper.Map<IEnumerable<RegionDto>>(regionDomain));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);
            if (regionDomain is null)
                return NotFound();
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
    }
}
