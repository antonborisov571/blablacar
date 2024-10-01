using Blablacar.Contracts.Requests.Trips.DeletePassenger;
using Blablacar.Contracts.Requests.Trips.DeleteTrip;
using Blablacar.Contracts.Requests.Trips.GetTrip;
using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Contracts.Requests.Trips.GetTripsUser;
using Blablacar.Contracts.Requests.Trips.PostCreateTrip;
using Blablacar.Contracts.Requests.Trips.PostCreateTripNotification;
using Blablacar.Contracts.Requests.Trips.PostTripBooking;
using Blablacar.Core.Requests.Trips.DeletePassenger;
using Blablacar.Core.Requests.Trips.DeleteTrip;
using Blablacar.Core.Requests.Trips.GetTrip;
using Blablacar.Core.Requests.Trips.GetTrips;
using Blablacar.Core.Requests.Trips.GetTripsUser;
using Blablacar.Core.Requests.Trips.PostCreateTrip;
using Blablacar.Core.Requests.Trips.PostCreateTripNotification;
using Blablacar.Core.Requests.Trips.PostTripBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за поездки 
/// </summary>
[ApiController]
[Route("api/[controller]/")]
public class TripsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создание поездки
    /// </summary>
    /// <param name="request"><see cref="PostCreateTripRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpPost("createTrip")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task CreateTrip([FromBody] PostCreateTripRequest request, CancellationToken cancellationToken)
    {
        var command = new PostCreateTripCommand(request);
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Поиск поездок
    /// </summary>
    /// <param name="request"><see cref="PostCreateTripRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpGet("getTrips")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GetTripsResponse> GetTrips([FromQuery] GetTripsRequest request, CancellationToken cancellationToken)
    {
        var command = new GetTripsQuery(request);
        return await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Создание уведомлении о поездки
    /// </summary>
    /// <param name="request"><see cref="PostCreateTripNotificationRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpPost("createTripNotification")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task CreateTripNotification(
        [FromBody] PostCreateTripNotificationRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new PostCreateTripNotificationCommand(request);
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Создание уведомлении о поездки
    /// </summary>
    /// <param name="tripId">Id поездки</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="404">Если поездка не найдена</response>
    [HttpGet("{tripId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetTripResponse> GetTrip(int tripId, CancellationToken cancellationToken)
    {
        var command = new GetTripQuery(tripId);
        return await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получение поездок пользователя
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpGet("getTripsUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTripsUserResponse> GetTripsUser(CancellationToken cancellationToken)
    {
        var command = new GetTripsUserQuery();
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Бронирование поездки
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден или пользователь уже пассажир</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpPost("booking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Booking(PostTripBookingRequest request, CancellationToken cancellationToken)
    {
        var command = new PostTripBookingCommand(request);
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Удаление пассажира
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если что-то пошло не так</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpDelete("deletePassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task DeletePassenger([FromQuery] DeletePassengerRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePassengerCommand(request);
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Удаление поездки
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если что-то пошло не так</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [Authorize]
    [HttpDelete("deleteTrip")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task DeleteTrip([FromQuery] DeleteTripRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteTripCommand(request);
        await mediator.Send(command, cancellationToken);
    }
}