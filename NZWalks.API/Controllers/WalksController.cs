using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Request;
using NZWalks.API.Models.DTO.Response;
using NZWalks.API.Repositories.Abstractions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            
            Walk walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkResponseDto>(walkDomainModel));
            
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var WalkList = await walkRepository.GetAllWalksAsync(filterOn, filterQuery);

            return Ok(new
            {
                success = true,
                code = 200,
                data = mapper.Map<List<WalkResponseDto>>(WalkList)
            });
        }
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GatByIdAsynk(id);

            return Ok(new
            {
                success = true,
                code = 200,
                data = mapper.Map<WalkResponseDto>(walk)
            });

        }
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            
            var walk = mapper.Map<Walk>(updateWalkRequestDto);

            walk = await walkRepository.UpdateAsync(id, walk);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                code = 200,
                data = mapper.Map<WalkResponseDto>(walk)
            });
           
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync (id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                code = 200,
                data = mapper.Map<WalkResponseDto>(walk)
            });
        }
    }
}
