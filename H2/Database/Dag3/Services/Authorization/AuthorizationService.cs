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


    public bool IsAuthorizedAdmin(StaffRoles role)
    {

        //Check we have a user
        if (string.IsNullOrEmpty(_currentUserService.username))
        {
            return false;
        }

        //LINQ query for checking if the user has the role needed for the action
        var staff = _db.Staff.Where(s => s.Name == _currentUserService.username)
            .Select(s => s.Role)
            .FirstOrDefault();

        //We check the admin role up to the enum value for the admin role
        return Enum.TryParse(staff, ignoreCase: true, out StaffRoles parsed)
            && parsed == StaffRoles.Administrator;

    }
}