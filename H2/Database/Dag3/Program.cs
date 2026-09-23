namespace H2.Database.Dag3;
using Input;
using Services.Authorization;

public static class Program
{
    public static void Main(string[] args)
    {

        var context = Database.Connect();

        ICurrentUserService currentUserService = new CurrentUserService();
        currentUserService.SetUser("Alice");
        IAuthorizationService authorizationService = new AuthorizationService(context, currentUserService);

        Inserts.InsertAuthor(context, authorizationService);


    }
}