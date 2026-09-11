using BurgerX.Application.Auth;
using BurgerX.Application.Auth.Commands.Login;
using BurgerX.Application.Auth.Exceptions;
using BurgerX.Domain.Administration;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Application.Auth;

public class LoginCommandHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;
    private readonly ITokenService _tokenService;

    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _hasher = Substitute.For<IHasher>();
        _tokenService = Substitute.For<ITokenService>();

        _handler = new(_userRepository, _hasher, _tokenService);
    }

    private static User CreateUser(
        string userName = "joao",
        string hash = "hashed_password",
        bool isActive = true
    )
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = userName,
            Username = userName,
            Hash = hash,
            IsActive = isActive
        };
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowUserAuthException()
    {
        _userRepository.GetByUserName(Arg.Any<string>())
            .Returns((User?)null);

        var cmd = new LoginCommand("nonexistent", "any_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserAuthException>();
    }

    [Fact]
    public async Task Handle_WrongPassword_ShouldThrowUserAuthException()
    {
        var user = CreateUser();

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        var cmd = new LoginCommand(user.Username, "wrong_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserAuthException>();
    }

    [Fact]
    public async Task Handle_UserNotActive_ShouldThrowUserIsNotActiveException()
    {
        var user = CreateUser(isActive: false);

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        var cmd = new LoginCommand(user.Username, "correct_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserIsNotActiveException>();
    }

    [Fact]
    public async Task Handle_Success_ShouldReturnGeneratedToken()
    {
        var user = CreateUser();
        const string expectedToken = "generated.jwt.token";

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        _tokenService.Generate(user)
            .Returns(expectedToken);

        var cmd = new LoginCommand(user.Username, "correct_password");

        var result = await _handler.Handle(cmd, default);

        result.Should().Be(expectedToken);
    }

    [Fact]
    public async Task Handle_Success_ShouldCallHasherWithCorrectPasswordAndHash()
    {
        var user = CreateUser(hash: "stored_hash");

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify("correct_password", user.Hash)
            .Returns(true);

        var cmd = new LoginCommand(user.Username, "correct_password");

        await _handler.Handle(cmd, default);

        _hasher.Received(1).Verify("correct_password", user.Hash);
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldNotCallHasherOrTokenService()
    {
        _userRepository.GetByUserName(Arg.Any<string>())
            .Returns((User?)null);

        var cmd = new LoginCommand("nonexistent", "any_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserAuthException>();

        _hasher.DidNotReceive().Verify(Arg.Any<string>(), Arg.Any<string>());
        _tokenService.DidNotReceive().Generate(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_WrongPassword_ShouldNotCallTokenService()
    {
        var user = CreateUser();

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        var cmd = new LoginCommand(user.Username, "wrong_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserAuthException>();

        _tokenService.DidNotReceive().Generate(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_UserNotActive_ShouldNotCallTokenService()
    {
        var user = CreateUser(isActive: false);

        _userRepository.GetByUserName(user.Username)
            .Returns(user);

        _hasher.Verify(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        var cmd = new LoginCommand(user.Username, "correct_password");

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<UserIsNotActiveException>();

        _tokenService.DidNotReceive().Generate(Arg.Any<User>());
    }
}