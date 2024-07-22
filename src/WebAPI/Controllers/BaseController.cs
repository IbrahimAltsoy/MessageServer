using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");
    private IMediator? _mediator;

    protected string? getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected Guid? getUserIdFromRequest() //todo authentication behavior?
    {
        Guid? userId = HttpContext.User.GetUserId();
        if(userId == null) throw new AuthorizationException("You are not authenticated.");
        return userId;
    }

    protected string? getUserEmailFromRequest() //todo authentication behavior?
    {
        string? userMail = HttpContext.User.GetUserEmail();
        if (userMail == null) throw new AuthorizationException("You are not authenticated.");
        return userMail;
    }

    protected List<string> getUserRolesFromRequest() //todo authentication behavior?
    {
        List<string>? roles = HttpContext.User.ClaimRoles();
        return roles;
    }
}
