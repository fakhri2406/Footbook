using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class StadiumController : ControllerBase
{
    private readonly IStadiumService _stadiumService;
    
    public StadiumController(IStadiumService stadiumService) => _stadiumService = stadiumService;
    
    #region GET
    
    /// <summary>
    /// Get all stadiums
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Stadium.GetAll)]
    [ProducesResponseType(typeof(IEnumerable<CreateStadiumResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _stadiumService.GetAllAsync();
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get a stadium by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Stadium.GetById)]
    [ProducesResponseType(typeof(CreateStadiumResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _stadiumService.GetByIdAsync(id);
        return Ok(response);
    }

    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new stadium
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Stadium.Create)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateStadiumResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateStadiumRequest request)
    {
        var response = await _stadiumService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), null, response);
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Update an existing stadium
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Stadium.Update)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UpdateStadiumResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStadiumRequest request)
    {
        var response = await _stadiumService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Delete a stadium
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Stadium.Delete)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _stadiumService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
} 