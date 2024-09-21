using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Request;
using NZWalks.API.Models.DTO.Response;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Abstractions;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this.logger = logger;
        }



        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAllRegion Action Method was invoked");
            
            var regions = await _regionRepository.GetAllAsync();

            logger.LogInformation($"Finish GetAllRegions request with data:  {JsonSerializer.Serialize(regions)}");

            var regionDto = _mapper.Map<List<RegionResponseDto>>(regions);
            return Ok(regionDto);
        }
        
        
        
        [HttpGet]
        [Authorize(Roles = "Reader")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //var region = await _context.Regions.FindAsync(id);
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            
            var regionDto = _mapper.Map<RegionResponseDto>(region);
            return Ok(regionDto);
        }

        
        
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Save([FromBody] AddRegionRequestDto addRegionDto)
        {
            var regionDomainModel = _mapper.Map<Region>(addRegionDto);

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            var regionDto = _mapper.Map<RegionResponseDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }



        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:guid}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto )
        {
            var RegionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
            RegionDomainModel = await _regionRepository.UpdateAsync(id, RegionDomainModel);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            var RegionResponseModel = _mapper.Map<RegionResponseDto>(RegionDomainModel);

            return Ok(RegionResponseModel);
           
        }

        
        
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            var RegionDomainModel = await _regionRepository.DeleteAsync(id);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionResponseDto>(RegionDomainModel);
            return Ok(regionDto);
        }
    }
}
