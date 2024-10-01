using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blablacar.Core.Requests.Auth.PostRegister;
using Blablacar.Core.Requests.Auth.PostResetPassword;
using Blablacar.Contracts.Requests.Account.PostRefreshToken;
using Blablacar.Contracts.Requests.Auth.PostConfirmEmail;
using Blablacar.Contracts.Requests.Auth.PostConfirmPasswordReset;
using Blablacar.Contracts.Requests.Auth.PostForgotPassword;
using Blablacar.Contracts.Requests.Auth.PostLogin;
using Blablacar.Contracts.Requests.Auth.PostRefreshToken;
using Blablacar.Contracts.Requests.Auth.PostRegister;
using Blablacar.Contracts.Requests.Auth.PostResetPassword;
using Blablacar.Contracts.Requests.Auth.PostRevokeToken;
using Blablacar.Contracts.Requests.Auth.PostTwoFactor;
using Blablacar.Core.Requests.Auth.PostConfirmEmail;
using Blablacar.Core.Requests.Auth.PostConfirmPasswordReset;
using Blablacar.Core.Requests.Auth.PostForgotPassword;
using Blablacar.Core.Requests.Auth.PostLogin;
using Blablacar.Core.Requests.Auth.PostRefreshToken;
using Blablacar.Core.Requests.Auth.PostRevokeToken;
using Blablacar.Core.Requests.Auth.PostTwoFactor;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за авторизацию и регистрацию
/// </summary>
[ApiController]
[Route("api/[controller]/")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Логинит пользователя и возвращает JWT токен, если пользователь залогинился
    /// </summary>
    /// <param name="request">Post Login Request(Email и Password)</param>
    /// <param name="cancellationToken"></param>
    /// <returns>JWT токен</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если у пользователя некорректные данные, которые не прошли валидацию</response>
    /// <response code="401">Если у пользователя некорректные данные(неверная почта или пароль)</response>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<PostLoginResponse> Login([FromBody] PostLoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostLoginCommand(request);
        var response = await _mediator.Send(command, cancellationToken);
        return response;
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="request">Post Register Request(Email, UserName, Password, PasswordConfirm)</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если у пользователя некорректные данные, которые не прошли валидацию</response>
    /// <response code="500">Если пользователь уже есть в системе</response>
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PostRegisterResponse> Register([FromBody] PostRegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostRegisterCommand(request);
        return await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Забыл пароль
    /// </summary>
    /// <param name="request">Post ForgotPassword Request(Email)</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если у пользователя некорректные данные, которые не прошли валидацию</response>
    [HttpPost("ForgotPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ForgotPassword([FromBody] PostForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostForgotPasswordCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Сброс пароля
    /// </summary>
    /// <param name="request">Post Reset Request(Email, NewPassword, NewPasswordConfirm)</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если у пользователя некорректные данные, которые не прошли валидацию</response>
    /// <response code="500">Если не получилось изменить пароль</response>
    [HttpPost("ResetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PostResetPasswordResponse> ResetPassword([FromBody] PostResetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostResetPasswordCommand(request);
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление JWT
    /// </summary>
    /// <param name="request">Post Refresh Token Request(JWT, Refresh Token)</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Post Refresh Token Response(JWT, Refresh Token)</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если у пользователя просрочен RefreshToken или в JWT нет нужных Claims</response>
    [HttpPost("RefreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<PostRefreshTokenResponse> RefreshToken([FromBody] PostRefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostRefreshTokenCommand(request);
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление Refresh Token
    /// </summary>
    /// <param name="request">PostRevokeTokenRequest(Email)</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователя с таким Email нет</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpPost("RevokeToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task RevokeToken([FromBody] PostRevokeTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new PostRevokeTokenCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Подтверждение почты
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь ввёл неверный код подтверждение</response>
    [HttpPost("ConfirmEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ConfirmEmail([FromBody] PostConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostConfirmEmailCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Подтверждение сброса пароля
    /// </summary>
    /// <param name="request">PostConfirmPasswordResetRequest()</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь ввёл неверный код подтверждение</response>
    [HttpPost("ConfirmPasswordReset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ConfirmPasswordReset([FromBody] PostConfirmPasswordResetRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostConfirmPasswordResetCommand(request);
        await _mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Двухфакторка
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь ввёл неверный код подтверждение</response>
    [HttpPost("TwoFactor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<PostTwoFactorResponse> TwoFactor([FromBody] PostTwoFactorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PostTwoFactorCommand(request);
        return await _mediator.Send(command, cancellationToken);
    }
}