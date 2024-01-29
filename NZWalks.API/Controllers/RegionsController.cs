using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Viewer, Admin")]
    public async Task<IActionResult> GetAll()
    {
        var regions = await _regionRepository.GetAllAsync();
        var regionsDto = _mapper.Map<List<RegionDto>>(regions);

        return Ok(regionsDto);
    }

    [HttpGet]
    [Authorize(Roles = "Viewer, Admin")]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetById(id);
        if (region is null)
        {
            return NotFound(new { error = "No Region found" });
        }

        var regionDto = _mapper.Map<RegionDto>(region);

        return Ok(regionDto);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] NewRegionDto regionDto)
    {
        var regionModel = _mapper.Map<Region>(regionDto);
        var region = await _regionRepository.CreateAsync(regionModel);

        var newRegionDto = _mapper.Map<RegionDto>(region);

        return CreatedAtAction(nameof(GetById), new { id = region.Id }, newRegionDto);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] NewRegionDto regionDto)
    {
        var regionModel = _mapper.Map<Region>(regionDto);
        var region = await _regionRepository.Update(id, regionModel);
        if (region is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {
        var region = await _regionRepository.Remove(id);
        if (region is null)
        {
            return NotFound();
        }

        var deletedRegionDto = _mapper.Map<RegionDto>(region);

        return Ok(deletedRegionDto);
    }
}