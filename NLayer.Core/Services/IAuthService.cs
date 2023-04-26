using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IAuthService
    {
        Task<CustomResponseDto<UserDto>> Register(UserDto userInfo, UserCredentialsDto userCredentials);
        CustomResponseDto<string> Login(UserCredentialsDto userCredentials);
        string CreateToken(User user);
        void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
        bool VerifyPasswordHash(string password, byte[] hash, byte[] salt);
    }
}
