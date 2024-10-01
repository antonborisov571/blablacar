using AutoFixture;
using Blablacar.Contracts.Requests.Auth.PostForgotPassword;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Requests.Auth.PostForgotPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Blablacar.Tests.HandlerTests;

public class PostForgotPasswordCommandHandlerTests
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
    private readonly Mock<IEmailSender> _emailSender = new();
    private readonly Mock<ILogger<PostForgotPasswordCommandHandler>> _logger = new();
    private readonly PostForgotPasswordCommandHandler _postConfirmPasswordResetCommandHandler;

    public PostForgotPasswordCommandHandlerTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _postConfirmPasswordResetCommandHandler = new PostForgotPasswordCommandHandler(
            _userManager.Object,
            _emailSender.Object,
            _logger.Object
        );
    }
    
    [Fact]
    public async Task Handle_RequestNull()
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            await _postConfirmPasswordResetCommandHandler.Handle(null!, new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_UnconfirmedEmailException()
    {
        // Arrange
        var user = _fixture.Build<User>().Create();

        _userManager.Setup(x => x.FindByEmailAsync(user.Email!))
            .Returns(() => Task.FromResult(user)!);
        
        // Act & Assert
        await Assert.ThrowsAsync<UnconfirmedEmailException>(async () => 
            await _postConfirmPasswordResetCommandHandler.Handle(
                new PostForgotPasswordCommand(new PostForgotPasswordRequest
                    {Email = user.Email!}),
                new CancellationToken()));
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var user = _fixture.Build<User>().Do(x => x.EmailConfirmed = true).Create();

        _userManager.Setup(x => x.FindByEmailAsync(user.Email!))
            .Returns(() => Task.FromResult(user)!);

        _userManager.Setup(x => x.IsEmailConfirmedAsync(user))
            .ReturnsAsync(true);
        
        // Act & Assert
        try
        {
            await _postConfirmPasswordResetCommandHandler.Handle(
                new PostForgotPasswordCommand(new PostForgotPasswordRequest
                    {Email = user.Email!}),
                new CancellationToken());
        }
        catch (Exception ex)
        {
            Assert.True(false, $"An unexpected exception was thrown: {ex}");
        }
    }
}