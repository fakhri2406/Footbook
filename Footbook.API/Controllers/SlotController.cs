using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class SlotController : ControllerBase
{
    private readonly ISlotService _slotService;
    
    public SlotController(ISlotService slotService) => _slotService = slotService;
    
    #region GET
    
    /// <summary>
    /// Get all slots
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Slot.GetAll)]
    [ProducesResponseType(typeof(IEnumerable<CreateSlotResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _slotService.GetAllAsync();
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get a slot by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Slot.GetById)]
    [ProducesResponseType(typeof(CreateSlotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _slotService.GetByIdAsync(id);
        return Ok(response);
    }
    
    /// <summary>
    /// Get slots by field ID
    /// </summary>
    /// <param name="fieldId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Slot.GetByField)]
    [ProducesResponseType(typeof(IEnumerable<CreateSlotResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByField([FromRoute] Guid fieldId)
    {
        var responses = await _slotService.GetByFieldIdAsync(fieldId);
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new slot
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Slot.Create)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateSlotResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateSlotRequest request)
    {
        var response = await _slotService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), null, response);
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Update an existing slot
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Slot.Update)]
    [ProducesResponseType(typeof(UpdateSlotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSlotRequest request)
    {
        var response = await _slotService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Delete a slot
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Slot.Delete)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _slotService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
} 