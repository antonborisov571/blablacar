using AutoFixture;
using AutoMapper;
using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Profiles;
using Blablacar.Core.Requests.Trips.GetTrip;
using Blablacar.Core.Requests.Trips.GetTrips;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class GetTripsQueryHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AppMappingProfile())));
    private readonly Mock<IAvatarService> _avatarService = new();
    private readonly Mock<AbstractTripsRepository> _tripsRepository = new(new Mock<IDbContext>().Object);
    private readonly Mock<ILogger<GetTripsQueryHandler>> _logger = new();
    private readonly GetTripsQueryHandler _getTripsQueryHandler;

    public GetTripsQueryHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _getTripsQueryHandler = new GetTripsQueryHandler(
            _tripsRepository.Object,
            _mapper,
            _avatarService.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _getTripsQueryHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var trips = _fixture.Build<Trip>().CreateMany(5).ToList();

        _tripsRepository.Setup(x => x.GetTripsByRequest(It.IsAny<GetTripsRequest>()))
            .ReturnsAsync(trips);
        
        // Act
        var response = await _getTripsQueryHandler.Handle(
            new GetTripsQuery(new GetTripsRequest()),
            new CancellationToken());
        
        // Assert 
        Assert.Equivalent(response.Trips, _mapper.Map<List<GetTripsResponseItem>>(trips));
    }
}