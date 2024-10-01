using AutoFixture;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Requests.Account.GetUserInfo;
using Blablacar.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class GetUserInfoQueryHandlerTests
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
    private readonly Mock<ISftpService> _sftpService = new();
    private readonly Mock<ILogger<GetUserInfoQueryHandler>> _logger = new();
    private readonly GetUserInfoQueryHandler _getUserInfoQueryHandler;

    public GetUserInfoQueryHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _getUserInfoQueryHandler = new GetUserInfoQueryHandler(
            _userManager.Object,
            _userContext.Object,
            _sftpService.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _getUserInfoQueryHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_NotFoundUserId()
    {
        // Arrange
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => null);
        
        // Assert
        await Assert.ThrowsAsync<CurrentUserIdNotFound>(async () => 
            await _getUserInfoQueryHandler.Handle(new GetUserInfoQuery(), new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => guid);
        
        var user = _fixture.Build<User>().Create();

        _userManager.Setup(x => x.FindByIdAsync(guid.ToString()))
            .Returns(() => Task.FromResult(user)!);
        
        // Act
        var response = await _getUserInfoQueryHandler.Handle(new GetUserInfoQuery(), new CancellationToken());
        
        // Assert
        Assert.Equal(response.FirstName, user.FirstName);
    }
}