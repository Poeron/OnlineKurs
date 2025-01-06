using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;

public class BaseController : Controller
{
    private readonly JwtService _jwtService;

    public BaseController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    protected bool IsTeacher()
    {
        var jwtToken = HttpContext.Session.GetString("jwtToken");
        if (string.IsNullOrEmpty(jwtToken)) return false;

        var role = _jwtService.GetUserRoleFromToken(jwtToken);
        return role == "teacher";
    }

    protected bool IsUser()
    {
        var jwtToken = HttpContext.Session.GetString("jwtToken");
        if (string.IsNullOrEmpty(jwtToken)) return false;

        var role = _jwtService.GetUserRoleFromToken(jwtToken);
        return role == "user";
    }
}
