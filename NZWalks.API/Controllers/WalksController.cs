using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            Walk walkDomainModel = mapper.Map<Walk>(addWalkRequestDto); 
            
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkResponseDto>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var WalkList = await walkRepository.GetAllWalksAsync();

            return Ok(new
            {
                success = true,
                code = 200,
                data = mapper.Map<List<WalkResponseDto>>(WalkList)
            });
        }
    }
}
