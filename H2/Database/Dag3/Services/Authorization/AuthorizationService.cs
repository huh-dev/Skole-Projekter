using Dag3.Entities;
using H2.Database.Dag3.Enums;
namespace H2.Database.Dag3.Services.Authorization;

public class AuthorizationService : IAuthorizationService
{

    private readonly AppDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationService(AppDbContext db, ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }


    public bool IsAuthorizedStaff()
    {
        if (string.IsNullOrEmpty(_currentUserService.username))
        {
            return false;
        }

        return _db.Staff.Any(s => s.Name == _currentUserService.username);
    }

    public bool IsAuthorizedAdmin(StaffRoles role)
    {
        if (string.IsNullOrEmpty(_currentUserService.username))
        {
            return false;
        }

        var staffRole = _db.Staff.Where(s => s.Name == _currentUserService.username)
            .Select(s => s.Role)
            .FirstOrDefault();

        return Enum.TryParse(staffRole, ignoreCase: true, out StaffRoles parsed)
            && parsed == StaffRoles.Administrator;
    }
}