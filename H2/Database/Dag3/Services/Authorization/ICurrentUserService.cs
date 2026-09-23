namespace H2.Database.Dag3.Services.Authorization;

public interface ICurrentUserService
{
    public string? username { get; set; }

    public void SetUser(string username);
}