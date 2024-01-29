using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _repository;
    private readonly ILogger<WalksController> _logger;

    public WalksController(IMapper mapper, IWalkRepository repository, ILogger<WalksController> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
    {
       
           
            _logger.LogInformation("Getting all walks...");
            var walks = await _repository.GetAll(filterOn, filterQuery, sortBy, isAscending ?? true,  pageNumber, pageSize);
            var walkDto = _mapper.Map<List<NewWalkDto>>(walks);
            _logger.LogInformation($"Results: {JsonSerializer.Serialize(walkDto)}");
            return Ok(walkDto);
        
        
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var walk = await _repository.GetById(id);
            if (walk is null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<NewWalkDto>(walk);
            return Ok(walkDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] UpdateWalkDto walkDto)
    {
        
        var walkModel = _mapper.Map<Walk>(walkDto);
        var walk = await _repository.CreateAsync(walkModel);
        var newWalkDto = _mapper.Map<NewWalkDto>(walk);
        return CreatedAtAction("GetById", new { id = walk.Id }, newWalkDto);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateWalkDto walkDto)
    {
        

            var walkModel = _mapper.Map<Walk>(walkDto);
            await _repository.Update(id, walkModel);
            return NoContent();
        
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        
            var walk = await _repository.Remove(id);
            if (walk is null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<NewWalkDto>(walk);
        
            return Ok(walkDto);
       
    }
}