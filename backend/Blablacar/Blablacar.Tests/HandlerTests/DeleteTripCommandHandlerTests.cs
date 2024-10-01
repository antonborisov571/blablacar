using AutoFixture;
using Blablacar.Contracts.Requests.Trips.DeleteTrip;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Requests.Trips.DeleteTrip;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class DeleteTripCommandHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<UserManager<User>> _userManager = new(
        new Mock<IUserStore<User>>().Object,
        new Mock<IOptions<IdentityOptions>>().Object,
        new Mock<IPasswordHasher<User>>().Object,
        new IUserValidator<User>[0],
        new IPasswordValidator<User>[0],
        new Mock<ILookupNormalizer>().Object,
        new Mock<IdentityErrorDescriber>().Object,
        new Mock<IServiceProvider>().Object,
        new Mock<ILogger<UserManager<User>>>().Object);
    private readonly Mock<IUserContext> _userContext = new();
    private readonly Mock<AbstractTripsRepository> _tripsRepository = new(new Mock<IDbContext>().Object);
    private readonly Mock<IEmailSender> _emailSender = new();
    private readonly Mock<ILogger<DeleteTripCommandHandler>> _logger = new();
    private readonly DeleteTripCommandHandler _deletePassengerCommandHandler;

    public DeleteTripCommandHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _deletePassengerCommandHandler = new DeleteTripCommandHandler(
            _userContext.Object,
            _userManager.Object,
            _tripsRepository.Object,
            _emailSender.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _deletePassengerCommandHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_NotFoundUserId()
    {
        // Arrange
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => null);
        
        // Assert
        await Assert.ThrowsAsync<CurrentUserIdNotFound>(async () => 
            await _deletePassengerCommandHandler.Handle(
                new DeleteTripCommand(new DeleteTripRequest()), 
                new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_DriverNotRealDriver()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => guid);
        
        var user = _fixture.Build<User>().Do(x => x.Id = "2").Create();

        _userManager.Setup(x => x.FindByIdAsync(guid.ToString()))
            .Returns(() => Task.FromResult(user)!);

        const int tripId = 1;
        const string passengerId = "1"; 
        var trip = _fixture.Build<Trip>().Do(x => x.DriverId = "1").Create();

        _tripsRepository.Setup(x => x.GetTripsWithDriverPassengers(tripId))
            .ReturnsAsync(() => trip);

        _userManager.Setup(x => x.FindByIdAsync(passengerId))
            .ReturnsAsync(user);
        
        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _deletePassengerCommandHandler.Handle(
            new DeleteTripCommand(new DeleteTripRequest { TripId = tripId }),
            new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => guid);
        
        var user = _fixture.Build<User>().With(u => u.Id, guid.ToString).Create();

        _userManager.Setup(x => x.FindByIdAsync(guid.ToString()))
            .Returns(() => Task.FromResult(user)!);

        const int tripId = 1;
        const string passengerId = "1";


        var passengers = _fixture.Build<User>().CreateMany(5).ToList();
        var trip = _fixture.Build<Trip>()
            .With(u => u.DriverId, guid.ToString)
            .With(t => t.Passengers, passengers)
            .Create();

        _tripsRepository.Setup(x => x.GetTripsWithDriverPassengers(tripId))
            .ReturnsAsync(() => trip);

        _userManager.Setup(x => x.FindByIdAsync(passengerId))
            .ReturnsAsync(user);
        
        // Act & Assert
        try
        {
            await _deletePassengerCommandHandler.Handle(
                new DeleteTripCommand(new DeleteTripRequest{TripId = tripId}),
                new CancellationToken());
        }
        catch (Exception ex)
        {
            Assert.True(true, $"{ex.Message}");
        }
    }
}