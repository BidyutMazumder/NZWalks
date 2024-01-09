﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Request;
using NZWalks.API.Models.DTO.Response;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Abstractions;

namespace NZWalks.API.Controllers
{
    //https://localhost:44305/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var regions = await _regionRepository.GetAllAsync();
            // map to dto
            //var regionDto = new List<RegionResponseDto>();
            //foreach (var region in regions)
            //{
            //    regionDto.Add(new RegionResponseDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionDto = _mapper.Map<List<RegionResponseDto>>(regions);
            return Ok(regionDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //var region = await _context.Regions.FindAsync(id);
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            //var regionDto = new RegionResponseDto()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionResponseDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] AddRegionRequestDto addRegionDto)
        {
            //var regionDomainModel = new Region()
            //{
            //    Code = addRegionDto.Code,
            //    Name = addRegionDto.Name,
            //    RegionImageUrl = addRegionDto.RegionImageUrl
            //};

            var regionDomainModel = _mapper.Map<Region>(addRegionDto);


            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //var regionDto = new RegionResponseDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = _mapper.Map<RegionResponseDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto )
        {

            //var RegionDomainModel = new Region();
            //RegionDomainModel.Name = updateRegionRequestDto.Name;
            //RegionDomainModel.Code = updateRegionRequestDto.Code;
            //RegionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            var RegionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
            RegionDomainModel = await _regionRepository.UpdateAsync(id, RegionDomainModel);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            //var RegionResponseModel = new RegionResponseDto();
            //RegionResponseModel.Name = RegionDomainModel.Name;
            //RegionResponseModel.Code = RegionDomainModel.Code;
            //RegionResponseModel.RegionImageUrl = RegionDomainModel.RegionImageUrl;

            var RegionResponseModel = _mapper.Map<RegionResponseDto>(RegionDomainModel);

            return Ok(RegionResponseModel);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            var RegionDomainModel = await _regionRepository.DeleteAsync(id);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionResponseDto()
            //{
            //    Name = RegionDomainModel.Name,
            //    Code = RegionDomainModel.Code,
            //    RegionImageUrl = RegionDomainModel.RegionImageUrl
            //};

            var regionDto = _mapper.Map<RegionResponseDto>(RegionDomainModel);
            return Ok(regionDto);
        }
    }
}
