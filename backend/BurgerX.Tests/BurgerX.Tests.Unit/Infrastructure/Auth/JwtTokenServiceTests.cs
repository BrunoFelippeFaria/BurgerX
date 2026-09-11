using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using BurgerX.Domain.Administration;
using BurgerX.Infrastructure.Auth;

using FluentAssertions;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using NSubstitute;

namespace BurgerX.Tests.Unit.Infrastructure.Auth;

public class JwtTokenServiceTests
{
    private readonly IOptions<JwtOptions> _options;
    private readonly JwtTokenService _jwtTokenService;

    private readonly JwtOptions _jwtOptions = new()
    {
        ExpirationDays = 30,
        SecretKey = "test_secret_key_with_more_than_256_bits"
    };

    public JwtTokenServiceTests()
    {
        _options = Substitute.For<IOptions<JwtOptions>>();
        _options.Value.Returns(_jwtOptions);

        _jwtTokenService = new(_options);
    }

    private static User CreateUser(Guid? id = null, string name = "joao")
    {
        return new User()
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Username = name,
            Hash = ""
        };
    }

    [Fact]
    public void Generate_ShouldReturnNonEmptyToken()
    {
        var user = CreateUser();

        var token = _jwtTokenService.Generate(user);

        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Generate_ShouldReturnValidJwtFormat()
    {
        var user = CreateUser();

        var token = _jwtTokenService.Generate(user);

        var handler = new JwtSecurityTokenHandler();
        handler.CanReadToken(token).Should().BeTrue();
    }

    [Fact]
    public void Generate_ShouldIncludeUserIdClaim()
    {
        var user = CreateUser();

        var token = _jwtTokenService.Generate(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        idClaim.Should().NotBeNull();
        idClaim!.Value.Should().Be(user.Id.ToString());
    }

    [Fact]
    public void Generate_ShouldIncludeNameClaim()
    {
        var user = CreateUser(name: "ana");

        var token = _jwtTokenService.Generate(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var nameClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        nameClaim.Should().NotBeNull();
        nameClaim!.Value.Should().Be("ana");
    }

    [Fact]
    public void Generate_ShouldSetExpirationAccordingToOptions()
    {
        var user = CreateUser();
        var before = DateTime.UtcNow;

        var token = _jwtTokenService.Generate(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var expectedExpiration = before.AddDays(_jwtOptions.ExpirationDays);

        jwt.ValidTo.Should().BeCloseTo(expectedExpiration, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Generate_ShouldSetNotBeforeToUtcNow()
    {
        var user = CreateUser();
        var before = DateTime.UtcNow;

        var token = _jwtTokenService.Generate(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        jwt.ValidFrom.Should().BeCloseTo(before, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Generate_ShouldSignWithHmacSha256()
    {
        var user = CreateUser();

        var token = _jwtTokenService.Generate(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        jwt.SignatureAlgorithm.Should().Be(SecurityAlgorithms.HmacSha256Signature);
    }

    [Fact]
    public void Generate_DifferentUsers_ShouldProduceDifferentTokens()
    {
        var user1 = CreateUser(name: "User1");
        var user2 = CreateUser(name: "User2");

        var token1 = _jwtTokenService.Generate(user1);
        var token2 = _jwtTokenService.Generate(user2);

        token1.Should().NotBe(token2);
    }
}