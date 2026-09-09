
using BurgerX.Application.Auth.Exceptions;

using Mediator;

namespace BurgerX.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IHasher hasher,
    ITokenService tokenService
)
    : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHasher _hasher = hasher;
    private readonly ITokenService _tokenService = tokenService;

    public async ValueTask<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName)
            ?? throw new UserAuthException();

        var passwordIsCorrect = _hasher.Verify(request.Password, user.Hash);

        if (!passwordIsCorrect)
            throw new UserAuthException();

        if (!user.IsActive)
            throw new UserIsNotActiveException();
            
        var token = _tokenService.Generate(user);
        return token;
    }
}