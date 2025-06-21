using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Team;
using Footbook.Core.DTOs.Responses.Team;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    
    public TeamController(ITeamService teamService) => _teamService = teamService;
    
    #region GET
    
    /// <summary>
    /// Get all teams
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Team.GetAll)]
    [ProducesResponseType(typeof(IEnumerable<CreateTeamResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _teamService.GetAllAsync();
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    /// <summary>
    /// Get a team by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Team.GetById)]
    [ProducesResponseType(typeof(CreateTeamResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _teamService.GetByIdAsync(id);
        return Ok(response);
    }
    
    /// <summary>
    /// Get members of a team
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(ApiEndPoints.Team.GetMembers)]
    [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMembers([FromRoute] Guid id)
    {
        var responses = await _teamService.GetMembersAsync(id);
        return responses.Any() ? Ok(responses) : NoContent();
    }
    
    #endregion
    
    #region POST
    
    /// <summary>
    /// Create a new team
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Team.Create)]
    [ProducesResponseType(typeof(CreateTeamResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateTeamRequest request)
    {
        var response = await _teamService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), null, response);
    }
    
    /// <summary>
    /// Add members to a team
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Team.AddMembers)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddMembers([FromRoute] Guid id, [FromForm] IEnumerable<Guid> userIds)
    {
        await _teamService.AddMembersAsync(id, userIds);
        return NoContent();
    }
    
    #endregion
    
    #region PUT
    
    /// <summary>
    /// Update an existing team
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route(ApiEndPoints.Team.Update)]
    [ProducesResponseType(typeof(UpdateTeamResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateTeamRequest request)
    {
        var response = await _teamService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    #endregion
    
    #region DELETE
    
    /// <summary>
    /// Remove a member from a team
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Team.RemoveMember)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveMember([FromRoute] Guid id, [FromRoute] Guid userId)
    {
        await _teamService.RemoveMemberAsync(id, userId);
        return NoContent();
    }
    
    /// <summary>
    /// Delete a team
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route(ApiEndPoints.Team.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _teamService.DeleteAsync(id);
        return NoContent();
    }
    
    #endregion
} 