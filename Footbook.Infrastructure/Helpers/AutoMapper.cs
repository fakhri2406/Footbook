using Footbook.Core.DTOs.Requests.Auth;
using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;
using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;
using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Core.DTOs.Requests.Booking;
using Footbook.Core.DTOs.Requests.Notification;
using Footbook.Core.DTOs.Responses.Booking;
using Footbook.Core.DTOs.Requests.Team;
using Footbook.Core.DTOs.Responses.Notification;
using Footbook.Core.DTOs.Responses.Team;
using Footbook.Data.Models;

namespace Footbook.Infrastructure.Helpers;

public static class AutoMapper
{
    #region Auth
    
    public static User MapToUser(this SignupRequest request, Guid roleId)
    {
        var salt = Guid.NewGuid().ToString();
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            SkillLevel = request.SkillLevel,
            PasswordSalt = salt,
            PasswordHash = Hasher.HashPassword(request.Password + salt),
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            RoleId = roleId
        };
    }
    
    #endregion
    
    #region Stadium
    
    public static Stadium MapToStadium(this CreateStadiumRequest request)
    {
        return new Stadium
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Branch = request.Branch,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };
    }
    
    public static CreateStadiumResponse MapToCreateStadiumResponse(this Stadium entity)
    {
        return new CreateStadiumResponse(
            entity.Name,
            entity.Branch,
            entity.Address,
            entity.Latitude,
            entity.Longitude);
    }
    
    public static Stadium MapToStadium(this UpdateStadiumRequest request, Guid id)
    {
        return new Stadium
        {
            Id = id,
            Name = request.Name,
            Branch = request.Branch,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };
    }
    
    public static UpdateStadiumResponse MapToUpdateStadiumResponse(this Stadium entity)
    {
        return new UpdateStadiumResponse(
            entity.Name,
            entity.Branch,
            entity.Address,
            entity.Latitude,
            entity.Longitude);
    }
    
    #endregion
    
    #region Field
    
    public static Field MapToField(this CreateFieldRequest request)
    {
        return new Field
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            FieldType = request.FieldType,
            StadiumId = request.StadiumId,
            Capacity = request.Capacity
        };
    }
    
    public static CreateFieldResponse MapToCreateFieldResponse(this Field entity)
    {
        return new CreateFieldResponse(
            entity.Name,
            entity.FieldType,
            entity.StadiumId,
            entity.Capacity);
    }
    
    public static Field MapToField(this UpdateFieldRequest request, Guid id)
    {
        return new Field
        {
            Id = id,
            Name = request.Name,
            FieldType = request.FieldType,
            StadiumId = request.StadiumId,
            Capacity = request.Capacity
        };
    }
    
    public static UpdateFieldResponse MapToUpdateFieldResponse(this Field entity)
    {
        return new UpdateFieldResponse(
            entity.Name,
            entity.FieldType,
            entity.StadiumId,
            entity.Capacity);
    }
    
    #endregion

    #region Slot
    
    public static Slot MapToSlot(this CreateSlotRequest request)
    {
        return new Slot
        {
            Id = Guid.NewGuid(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = request.Status,
            FieldId = request.FieldId
        };
    }
    
    public static CreateSlotResponse MapToCreateSlotResponse(this Slot entity)
    {
        return new CreateSlotResponse(
            entity.StartTime,
            entity.EndTime,
            entity.Status,
            entity.FieldId);
    }
    
    public static Slot MapToSlot(this UpdateSlotRequest request, Guid id)
    {
        return new Slot
        {
            Id = id,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = request.Status,
            FieldId = request.FieldId
        };
    }
    
    public static UpdateSlotResponse MapToUpdateSlotResponse(this Slot entity)
    {
        return new UpdateSlotResponse(
            entity.StartTime,
            entity.EndTime,
            entity.Status,
            entity.FieldId);
    }
    
    #endregion
    
    #region Booking
    
    public static Booking MapToBooking(this CreateBookingRequest request)
    {
        return new Booking
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            SlotId = request.SlotId,
            Status = Core.Enums.BookingStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static CreateBookingResponse MapToCreateBookingResponse(this Booking entity)
    {
        return new CreateBookingResponse(
            entity.UserId,
            entity.SlotId);
    }
    
    public static Booking MapToBooking(this UpdateBookingRequest request, Guid id)
    {
        return new Booking
        {
            Id = id,
            UserId = request.UserId,
            SlotId = request.SlotId
        };
    }
    
    public static UpdateBookingResponse MapToUpdateBookingResponse(this Booking entity)
    {
        return new UpdateBookingResponse(
            entity.UserId,
            entity.SlotId);
    }
    
    #endregion

    #region Team
    
    public static Team MapToTeam(this CreateTeamRequest request)
    {
        return new Team
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedByUserId = request.CreatedByUserId,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static CreateTeamResponse MapToCreateTeamResponse(this Team entity)
    {
        return new CreateTeamResponse(
            entity.Name,
            entity.CreatedByUserId,
            entity.TeamMembers.Select(tm => tm.UserId).ToList());
    }
    
    public static Team MapToTeam(this UpdateTeamRequest request, Guid id)
    {
        return new Team
        {
            Id = id,
            Name = request.Name
        };
    }
    
    public static UpdateTeamResponse MapToUpdateTeamResponse(this Team entity)
    {
        return new UpdateTeamResponse(
            entity.Name,
            entity.TeamMembers.Select(tm => tm.UserId).ToList());
    }
    
    #endregion

    #region Notification
    
    public static Notification MapToNotification(this CreateNotificationRequest request)
    {
        return new Notification
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Payload = request.Payload,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            UserId = request.UserId
        };
    }
    
    public static NotificationResponse MapToNotificationResponse(this Notification entity)
    {
        return new NotificationResponse(
            entity.Id,
            entity.Type,
            entity.Payload,
            entity.IsRead,
            entity.CreatedAt);
    }
    
    #endregion
}