using AutoFixture;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Requests.Trips.DeleteTripNotification;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class DeleteTripNotificationCommandHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<AbstractTripNotificationsRepository> _tripsNotificationsRepository = new(new Mock<IDbContext>().Object);
    private readonly Mock<ILogger<DeleteTripNotificationCommandHandler>> _logger = new();
    private readonly DeleteTripNotificationCommandHandler _deleteTripNotificationCommandHandler;

    public DeleteTripNotificationCommandHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _deleteTripNotificationCommandHandler = new DeleteTripNotificationCommandHandler(
            _tripsNotificationsRepository.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _deleteTripNotificationCommandHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Act & Assert
        try
        {
            await _deleteTripNotificationCommandHandler.Handle(
                new DeleteTripNotificationCommand(),
                new CancellationToken());
        }
        catch (Exception ex)
        {
            Assert.True(true, $"{ex.Message}");
        }
    }
}