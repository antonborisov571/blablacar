using AutoFixture;
using Blablacar.Contracts.Requests.Account.PatchUpdateUserInfo;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Requests.Account.PatchUpdateUserInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class PatchUpdateUserInfoCommandHandlerTests
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
    private readonly Mock<IUserClaimsManager> _userClaimsManager = new();
    private readonly Mock<IJwtGenerator> _jwtGenerator = new();
    private readonly Mock<IEmailSender> _emailSender = new();
    private readonly Mock<ILogger<PatchUpdateUserInfoCommandHandler>> _logger = new();
    private readonly PatchUpdateUserInfoCommandHandler _patchUpdateUserInfoCommandHandler;

    public PatchUpdateUserInfoCommandHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _patchUpdateUserInfoCommandHandler = new PatchUpdateUserInfoCommandHandler(
            _userManager.Object,
            _userContext.Object,
            _userClaimsManager.Object,
            _jwtGenerator.Object,
            _emailSender.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _patchUpdateUserInfoCommandHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_NotFoundUserId()
    {
        // Arrange
        _userContext.Setup(x => x.CurrentUserId)
            .Returns(() => null);
        
        // Assert
        await Assert.ThrowsAsync<CurrentUserIdNotFound>(async () => 
            await _patchUpdateUserInfoCommandHandler.Handle(
                new PatchUpdateUserInfoCommand(new PatchUpdateUserInfoRequest()), 
                new CancellationToken()));
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
        var response = await _patchUpdateUserInfoCommandHandler.Handle(
            new PatchUpdateUserInfoCommand(new PatchUpdateUserInfoRequest()), 
            new CancellationToken());
        
        // Assert
        Assert.Equal(response.AccessToken, user.AccessToken);
    }
}