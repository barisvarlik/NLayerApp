using AutoMapper;
using System.Text;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Configuration.Constants;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace NLayer.Service.Services
{
    public class AuthService : Service<User>, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using HMACSHA512 hmac = new();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expirationDate = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(claims: claims, expires: expirationDate, signingCredentials: signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public CustomResponseDto<string> Login(UserCredentialsDto userCredentials)
        {
            var user = _userRepository.GetUserByEmail(userCredentials.Email);
            if (user == null)
                return CustomResponseDto<string>.Fail(404, "User not found");
            bool verified = VerifyPasswordHash(userCredentials.Password, user.Credentials.PasswordHash, user.Credentials.PasswordSalt);
            if (!verified)
                return CustomResponseDto<string>.Fail(401, "Wrong credentials");
            var token = CreateToken(user);
            return CustomResponseDto<string>.Success(200, token);
        }

        public async Task<CustomResponseDto<UserDto>> Register(UserDto userInfo, UserCredentialsDto userCredentials)
        {
            CreatePasswordHash(userCredentials.Password, out byte[] hash, out byte[] salt);
            User userModel = new()
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                DateOfBirth = userInfo.DateOfBirth,
                Gender = userInfo.Gender,
                Credentials = new UserCredentials
                {
                    Email = userCredentials.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt
                }
            };
            var user = await AddAsync(userModel);
            var userDto = _mapper.Map<User, UserDto>(user);

            return CustomResponseDto<UserDto>.Success(200, userDto);
        }

        public bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using HMACSHA512 hmac = new(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
