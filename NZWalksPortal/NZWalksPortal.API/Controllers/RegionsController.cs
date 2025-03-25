using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksPortal.API.CustomActionFilter;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Models.DTO;
using NZWalksPortal.API.Repositories.Interface;
using System.Text.Json;

namespace NZWalksPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpPost("CreateRegion")]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto request)
        {
            var regionDomain = mapper.Map<Region>(request);
            regionDomain= await regionRepository.AddRegionAsync(regionDomain);
            var responseDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction(nameof(GetRegionById), new {id=responseDto.Id }, responseDto);
        }
        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            logger.LogInformation("GetAllRegions action method was involed");
            var regionDomain = await regionRepository.GetAllRegionsAsync();
            if(regionDomain is null)
                return NotFound();
            logger.LogInformation($"GetAllRegions response data: {JsonSerializer.Serialize(regionDomain)}");
            return Ok(mapper.Map<IEnumerable<RegionDto>>(regionDomain));
        }
        [HttpGet("{id}/GetRegionById")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);
            if (regionDomain is null)
                return NotFound();
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        [HttpDelete("{id}/DeleteRegion")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteRegionAsync(id);
            if (regionDomain is null)
                return NotFound();
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        [HttpPut("{id}/UpdateRegion")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto request)
        {
            var regionDomain = mapper.Map<Region>(request);
            regionDomain.Id = id;
            var updatedRegion = await regionRepository.UpdateRegionAsync(regionDomain);
            if (updatedRegion is null)
                return NotFound();
            return Ok(mapper.Map<RegionDto>(updatedRegion));
        }
    }
}
