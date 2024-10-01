using AutoFixture;
using AutoMapper;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Profiles;
using Blablacar.Core.Requests.Trips.GetTrip;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class GetTripQueryHandlerTests
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
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AppMappingProfile())));
    private readonly Mock<IAvatarService> _avatarService = new();
    private readonly Mock<AbstractTripsRepository> _tripsRepository = new(new Mock<IDbContext>().Object);
    private readonly Mock<IEmailSender> _emailSender = new();
    private readonly Mock<ILogger<GetTripQueryHandler>> _logger = new();
    private readonly GetTripQueryHandler _getTripQueryHandler;

    public GetTripQueryHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _getTripQueryHandler = new GetTripQueryHandler(
            _tripsRepository.Object,
            _mapper,
            _avatarService.Object,
            _userContext.Object,
            _userManager.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _getTripQueryHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_NotFoundTrip()
    {
        // Arrange
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => null);
        
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await _getTripQueryHandler.Handle(
                new GetTripQuery(1), 
                new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        const int tripId = 1;
        var trip = _fixture.Build<Trip>().With(x => x.Id, tripId).Create();

        _tripsRepository.Setup(x => x.GetTripsWithDriverPassengers(tripId))
            .ReturnsAsync(() => trip);
        
        // Act
        var response = await _getTripQueryHandler.Handle(
            new GetTripQuery(1),
            new CancellationToken());
        
        // Assert 
        Assert.Equal(response.Price, trip.Price);
    }
}