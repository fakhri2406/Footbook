using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Notification;
using Footbook.Core.DTOs.Responses.Notification;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    
    public NotificationController(INotificationService notificationService)
        => _notificationService = notificationService;
    
    #region GET
    
    /// <summary>
    /// Get notifications by user ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Notification.GetByUser)]
    [ProducesResponseType(typeof(IEnumerable<NotificationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
    {
        var responses = await _notificationService.GetByUserIdAsync(userId);
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new notification
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Notification.Create)]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        var response = await _notificationService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByUser), new { userId = request.UserId }, response);
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Mark a notification as read
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Notification.MarkAsRead)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkAsRead([FromRoute] Guid id)
    {
        await _notificationService.MarkAsReadAsync(id);
        return Ok();
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Delete a notification
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Notification.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _notificationService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
} 