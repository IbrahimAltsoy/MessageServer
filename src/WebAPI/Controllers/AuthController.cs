using Application.Features.Auth.Commands.CreatePhoneCode;
using Application.Features.Auth.Commands.EmailActivationCodeReSend;
using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.LoginPhone;
using Application.Features.Auth.Commands.PasswordReset;
using Application.Features.Auth.Commands.PhoneRegister;
using Application.Features.Auth.Commands.RefleshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RegisterPhoneCode;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Commands.VerifyEmail;
using Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Application.Features.Auth.Commands.VerifyPhone;
using Application.Features.Auth.Commands.VerifyPhoneSmsSendRequest;
using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
//Merhaba nasılsın
namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("WebAPIConfiguration").Get<WebApiConfiguration>();
    }   
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IPAddress = getIpAddress() };
        LoggedResponse? result = await Mediator.Send(loginCommand);

        if (result?.AccessToken.RefreshToken != "")
            setRefreshTokenToCookie(result.AccessToken.RefreshToken);

        return Ok(result.ToHttpResponse());
    }
    [HttpPost("LoginPhonePassword")]
    public async Task<IActionResult> LoginPhonePassword([FromBody] LoginPhonePasswordRequest request)
    {
        request.IPAddress = getIpAddress();
        LoginPhonePasswordResponse response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("CreatePhoneCode")]
    public async Task<IActionResult> CreatePhoneCode([FromBody] CreatePhoneCodeRequest request)
    {       
        CreatePhoneCodeResponse response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("LoginPhoneCode")]
    public async Task<IActionResult> LoginPhoneCode([FromBody] LoginPhoneCodeRequest request)
    {
        request.IPAddress = getIpAddress();
        LoginPhoneCodeResponse response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("RegisterPhone")]
    public async Task<IActionResult> RegisterPhone([FromBody]RegisterPhoneRequest request)
    {
        RegisterPhoneResponse response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Yeni kayıt işlemleri burada olacak.
    /// </summary>
    /// <remarks>
    /// Kayıt başarılı olunca 200 döner ve kullanıcıya bir email gönderir. Buradan token yanıtı almazsınız.
    /// </remarks>
    /// <returns>Başarılı olma durumu kontrol edilir.</returns>
    /// <response code="200">Kullanıcıya mail atar</response>
    /// <response code="400">Mevcut kullanıcı</response>
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, VerifyEmailUrlPrefix = $"{_configuration.ApiDomain}/Auth/VerifyEmail" };
        await Mediator.Send(registerCommand);
        return Ok("Eposta adresinizi doğrulayın");
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RefreshTokenCommand refreshTokenCommand = new() { RefleshToken = refreshToken, IPAddress = getIpAddress() };
        RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.AccessToken.RefreshToken);
        return Created(uri: "", result.ToHttpResponse());
    }

    [HttpGet("RefreshTokenForCookie")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new() { RefleshToken = getRefreshTokenFromCookies(), IPAddress = getIpAddress() };
        RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.AccessToken.RefreshToken);
        return Created(uri: "", result.ToHttpResponse());
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new() { Token = refreshToken, IPAddress = getIpAddress() };
        RevokedTokenResponse result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }

    [HttpPost("PasswordResetRequest")]
    public async Task<IActionResult> PasswordResetRequest([FromBody] PasswordResetRequestCommand passwordResetRequestCommand)
    {
        await Mediator.Send(passwordResetRequestCommand);
        return Ok("Mailinize gönderildi.");
    }    
    
    [HttpPost("PasswordReset")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommand passwordResetCommand)
    {
        await Mediator.Send(passwordResetCommand);
        return Ok("Başarıyla güncellendi. Giriş yapabilirsiniz.");
    }

    [HttpGet("EnableEmailAuthenticator")]
    public async Task<IActionResult> EnableEmailAuthenticator()
    {
        EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand =
            new() { UserId = (Guid)getUserIdFromRequest(), VerifyEmailUrlPrefix = $"{_configuration.ApiDomain}/Auth/VerifyEmailAuthenticator" };
        await Mediator.Send(enableEmailAuthenticatorCommand);

        return Ok();
    }

    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    [HttpGet("EnableOtpAuthenticator")]
    public async Task<IActionResult> EnableOtpAuthenticator()
    {
        EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new() { UserId = (Guid)getUserIdFromRequest() };
        EnabledOtpAuthenticatorResponse result = await Mediator.Send(enableOtpAuthenticatorCommand);

        return Ok(result);
    }

    [HttpPost("VerifyOtpAuthenticator")]
    public async Task<IActionResult> VerifyOtpAuthenticator([FromBody] string authenticatorCode)
    {
        VerifyOtpAuthenticatorCommand verifyEmailAuthenticatorCommand =
            new() { UserId = (Guid)getUserIdFromRequest(), ActivationCode = authenticatorCode };

        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    [HttpGet("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailCommand verifyEmailCommand)
    {
        await Mediator.Send(verifyEmailCommand);
        return Ok("Başarılı");
    }

    [HttpGet("VerifyPhone")]
    public async Task<IActionResult> VerifyPhone([FromQuery] string token)
    {
        VerifyPhoneCommand result = new VerifyPhoneCommand() { UserId = (Guid)getUserIdFromRequest(), Token = token };
        await Mediator.Send(result);
        return Ok();
    }

    [HttpGet("VerifyPhoneSmsSendRequest")]
    public async Task<IActionResult> VerifyPhoneSmsSendRequest()
    {
        VerifyPhoneSmsSendRequestCommand result = new VerifyPhoneSmsSendRequestCommand() { UserId = (Guid)getUserIdFromRequest() };
        await Mediator.Send(result);
        return Ok();
    }

    [HttpGet("EmailActivationCodeReSend")]
    public async Task<IActionResult> EmailActivationCodeReSend()
    {
        EmailActivationCodeReSendCommand result = new() { UserId = (Guid)getUserIdFromRequest(), VerifyEmailUrlPrefix = $"{_configuration.ApiDomain}/Auth/VerifyEmail" };
        await Mediator.Send(result);
        return Ok();
    }

    private string getRefreshTokenFromCookies() =>
        Request.Cookies["refreshToken"] ?? throw new ArgumentException("Refresh token is not found in request cookies.");

    private void setRefreshTokenToCookie(string refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key: "refreshToken", refreshToken, cookieOptions);
    }
}
