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
using Footbook.Core.DTOs.Requests.User;
using Footbook.Core.DTOs.Responses.Notification;
using Footbook.Core.DTOs.Responses.Team;
using Footbook.Core.DTOs.Responses.User;
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
            PhoneNumber = "+994" + request.PhoneNumber,
            SkillLevel = request.SkillLevel,
            PasswordHash = Hasher.HashPassword(request.Password + salt),
            PasswordSalt = salt,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            RoleId = roleId
        };
    }
    
    #endregion

    #region User
    
    public static User MapToUser(this UpdateUserRequest request, Guid id)
    {
        var salt = Guid.NewGuid().ToString();
        return new User
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = "+994" + request.PhoneNumber,
            PasswordHash = Hasher.HashPassword(request.Password + salt),
            PasswordSalt = salt,
            SkillLevel = request.SkillLevel
        };
    }

    public static UserResponse MapToUserResponse(this User entity)
    {
        return new UserResponse(
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.PhoneNumber,
            entity.SkillLevel,
            entity.ProfilePictureUrl
        );
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
    
    public static StadiumResponse MapToStadiumResponse(this Stadium entity)
    {
        return new StadiumResponse(
            entity.Name,
            entity.Branch,
            entity.Address,
            entity.Latitude,
            entity.Longitude,
            entity.ImageUrl);
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
    
    public static FieldResponse MapToFieldResponse(this Field entity)
    {
        return new FieldResponse(
            entity.Name,
            entity.FieldType,
            entity.StadiumId,
            entity.Capacity,
            entity.ImageUrl);
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
    
    public static SlotResponse MapToSlotResponse(this Slot entity)
    {
        return new SlotResponse(
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
    
    public static Booking MapToBooking(this UpdateBookingRequest request, Guid id)
    {
        return new Booking
        {
            Id = id,
            UserId = request.UserId,
            SlotId = request.SlotId
        };
    }
    
    public static BookingResponse MapToBookingResponse(this Booking entity)
    {
        return new BookingResponse(
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
    
    public static Team MapToTeam(this UpdateTeamRequest request, Guid id)
    {
        return new Team
        {
            Id = id,
            Name = request.Name
        };
    }
    
    public static TeamResponse MapToTeamResponse(this Team entity)
    {
        return new TeamResponse(
            entity.Name,
            entity.CreatedByUserId,
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