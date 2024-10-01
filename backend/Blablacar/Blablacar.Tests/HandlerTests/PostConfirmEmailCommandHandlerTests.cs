using AutoFixture;
using Blablacar.Contracts.Requests.Auth.PostConfirmEmail;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Requests.Auth.PostConfirmEmail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class PostConfirmEmailCommandHandlerTests
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
    private readonly Mock<ILogger<PostConfirmEmailCommandHandler>> _logger = new();
    private readonly PostConfirmEmailCommandHandler _postConfirmEmailCommandHandler;

    public PostConfirmEmailCommandHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _postConfirmEmailCommandHandler = new PostConfirmEmailCommandHandler(
            _userManager.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _postConfirmEmailCommandHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_WrongConfirmationTokenException()
    {
        // Arrange
        var user = _fixture.Build<User>().Create();

        const string code = "213";

        _userManager.Setup(x => x.FindByEmailAsync(user.Email!))
            .Returns(() => Task.FromResult(user)!);

        _userManager.Setup(x => x.UpdateAsync(user))
            .ReturnsAsync(() => IdentityResult.Success);

        _userManager.Setup(x => x.ConfirmEmailAsync(user, code))
            .ReturnsAsync(() => new IdentityResult());
        
        // Act & Assert
        await Assert.ThrowsAsync<WrongConfirmationTokenException>(async () => 
            await _postConfirmEmailCommandHandler.Handle(new PostConfirmEmailCommand(new PostConfirmEmailRequest
                    {Code = "213", Email = user.Email!}),
                new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var user = _fixture.Build<User>().Create();

        const string code = "213";

        _userManager.Setup(x => x.FindByEmailAsync(user.Email!))
            .Returns(() => Task.FromResult(user)!);

        _userManager.Setup(x => x.UpdateAsync(user))
            .ReturnsAsync(() => IdentityResult.Success);

        _userManager.Setup(x => x.ConfirmEmailAsync(user, code))
            .ReturnsAsync(IdentityResult.Success);
        
        // Act & Assert
        try
        {
            await _postConfirmEmailCommandHandler.Handle(
                new PostConfirmEmailCommand(new PostConfirmEmailRequest
                    {Code = "213", Email = user.Email!}),
                new CancellationToken());
        }
        catch (Exception ex)
        {
            Assert.True(false, $"An unexpected exception was thrown: {ex}");
        }
    }
}