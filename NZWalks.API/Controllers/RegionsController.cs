using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionsController> _logger;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Viewer, Admin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Calling GetALlRegions action method");
            var regions = await _regionRepository.GetAllAsync();
            var regionsDto = _mapper.Map<List<RegionDto>>(regions);
            _logger.LogInformation($"Finished with: {JsonSerializer.Serialize(regions)}");
            return Ok(regionsDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    [Authorize(Roles = "Viewer, Admin")]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var region = await _regionRepository.GetById(id);
            if (region is null)
            {
                return NotFound(new { error = "No Region found" });
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] NewRegionDto regionDto)
    {
        try
        {
            var regionModel = _mapper.Map<Region>(regionDto);
            var region = await _regionRepository.CreateAsync(regionModel);

            var newRegionDto = _mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, newRegionDto);

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] NewRegionDto regionDto)
    {
        try
        {
            var regionModel = _mapper.Map<Region>(regionDto);
            var region = await _regionRepository.Update(id, regionModel);
            if (region is null)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {
        try
        {
            var region = await _regionRepository.Remove(id);
            if (region is null)
            {
                return NotFound();
            }

            var deletedRegionDto = _mapper.Map<RegionDto>(region);

            return Ok(deletedRegionDto);
        }
        catch (Exception e)
        {
            _logger.LogError("error");
            throw;
        }
    }
}