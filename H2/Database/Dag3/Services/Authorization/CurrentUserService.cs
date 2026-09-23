namespace H2.Database.Dag3.Services.Authorization;

public class CurrentUserService : ICurrentUserService
{
    public string? username { get; set; }

    public void SetUser(string username)
    {
        this.username = username;
    }
}