using H2.Database.Dag3.Enums;
namespace H2.Database.Dag3.Services.Authorization;

public interface IAuthorizationService
{
    bool IsAuthorizedStaff();
    bool IsAuthorizedAdmin(StaffRoles role);
}