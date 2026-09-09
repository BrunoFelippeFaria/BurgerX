namespace BurgerX.Application.Auth;

public interface IHasher
{
    public string Hash(string value);
    public bool Verify(string value, string hash);
}