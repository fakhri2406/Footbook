using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class FieldController : ControllerBase
{
    private readonly IFieldService _fieldService;
    
    public FieldController(IFieldService fieldService) => _fieldService = fieldService;
    
    #region GET
    
    /// <summary>
    /// Get all fields
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Field.GetAll)]
    [ProducesResponseType(typeof(IEnumerable<CreateFieldResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _fieldService.GetAllAsync();
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get a field by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Field.GetById)]
    [ProducesResponseType(typeof(CreateFieldResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _fieldService.GetByIdAsync(id);
        return Ok(response);
    }
    
    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new field
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Field.Create)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateFieldResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateFieldRequest request)
    {
        var response = await _fieldService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), null, response);
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Update an existing field
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Field.Update)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UpdateFieldResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFieldRequest request)
    {
        var response = await _fieldService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Delete a field
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Field.Delete)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _fieldService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
} 