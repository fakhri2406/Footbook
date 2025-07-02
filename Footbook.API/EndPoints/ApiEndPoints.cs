namespace Footbook.API.EndPoints;

public static class ApiEndPoints
{
    private const string ApiBaseUrl = "api/v1";
    
    #region Auth
    
    public static class Auth
    {
        private const string BaseUrl = $"{ApiBaseUrl}/auth";
        
        public const string Signup = $"{BaseUrl}/signup";
        public const string Login = $"{BaseUrl}/login";
        public const string Refresh = $"{BaseUrl}/refresh";
        public const string Logout = $"{BaseUrl}/logout/{{userId::guid}}";
    }
    
    #endregion
    
    #region User
    
    public static class User
    {
        private const string BaseUrl = $"{ApiBaseUrl}/user";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Booking
    
    public static class Booking
    {
        private const string BaseUrl = $"{ApiBaseUrl}/booking";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string GetByUser = $"{BaseUrl}/user/{{userId::guid}}";
        public const string GetBySlot = $"{BaseUrl}/slot/{{slotId::guid}}";
        public const string Create = BaseUrl;
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Field
    
    public static class Field
    {
        private const string BaseUrl = $"{ApiBaseUrl}/field";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string Create = BaseUrl;
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Notification
    
    public static class Notification
    {
        private const string BaseUrl = $"{ApiBaseUrl}/notification";
        
        public const string GetByUser = $"{BaseUrl}/user/{{userId::guid}}";
        public const string Create = BaseUrl;
        public const string MarkAsRead = $"{BaseUrl}/{{id::guid}}/read";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Slot
    
    public static class Slot
    {
        private const string BaseUrl = $"{ApiBaseUrl}/slot";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string GetByField = $"{BaseUrl}/field/{{fieldId::guid}}";
        public const string Search = $"{BaseUrl}/search";
        public const string Create = BaseUrl;
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Stadium
    
    public static class Stadium
    {
        private const string BaseUrl = $"{ApiBaseUrl}/stadium";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string Create = BaseUrl;
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
    }
    
    #endregion
    
    #region Team
    
    public static class Team
    {
        private const string BaseUrl = $"{ApiBaseUrl}/team";
        
        public const string GetAll = BaseUrl;
        public const string GetById = $"{BaseUrl}/{{id::guid}}";
        public const string Create = BaseUrl;
        public const string Update = $"{BaseUrl}/{{id::guid}}";
        public const string Delete = $"{BaseUrl}/{{id::guid}}";
        public const string GetMembers = $"{BaseUrl}/{{id::guid}}/members";
        public const string AddMembers = $"{BaseUrl}/{{id::guid}}/members";
        public const string RemoveMember = $"{BaseUrl}/{{id::guid}}/members/{{userId::guid}}";
    }
    
    #endregion
} 