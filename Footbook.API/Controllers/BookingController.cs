using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Booking;
using Footbook.Core.DTOs.Responses.Booking;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    
    public BookingController(IBookingService bookingService) => _bookingService = bookingService;
    
    #region GET
    
    /// <summary>
    /// Get all bookings
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Booking.GetAll)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<CreateBookingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _bookingService.GetAllAsync();
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get a booking by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Booking.GetById)]
    [ProducesResponseType(typeof(CreateBookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _bookingService.GetByIdAsync(id);
        return Ok(response);
    }
    
    /// <summary>
    /// Get bookings by user ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Booking.GetByUser)]
    [ProducesResponseType(typeof(IEnumerable<CreateBookingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
    {
        var responses = await _bookingService.GetByUserIdAsync(userId);
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get bookings by slot ID
    /// </summary>
    /// <param name="slotId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Booking.GetBySlot)]
    [ProducesResponseType(typeof(IEnumerable<CreateBookingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySlot([FromRoute] Guid slotId)
    {
        var responses = await _bookingService.GetBySlotIdAsync(slotId);
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new booking
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Booking.Create)]
    [ProducesResponseType(typeof(CreateBookingResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateBookingRequest request)
    {
        var response = await _bookingService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), null, response);
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Update an existing booking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Booking.Update)]
    [ProducesResponseType(typeof(UpdateBookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBookingRequest request)
    {
        var response = await _bookingService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Delete a booking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Booking.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _bookingService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
}