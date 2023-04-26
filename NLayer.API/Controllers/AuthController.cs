using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _service;

        public AuthController(IMapper mapper, IAuthService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            return CreateActionResult(await _service.Register(registrationDto.User, registrationDto.Credentials));
        }

        [HttpPost("[action]")]
        public IActionResult Login(UserCredentialsDto userCred)
        {
            return CreateActionResult(_service.Login(userCred));
        }
    }
}


/*
{
  "user": {
    "firstName": "first",
    "lastName": "user",
    "dateOfBirth": "1999-01-01T00:00:00Z",
    "gender": "M"
  },
  "credentials": {
    "email": "string",
    "password": "string"
  }
} 
*/