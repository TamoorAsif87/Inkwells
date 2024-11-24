using Persistence.DTO;

namespace Persistence.contracts;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterDto registerDto);
    
}
