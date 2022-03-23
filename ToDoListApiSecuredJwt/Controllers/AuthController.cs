using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Data;
using ToDoListApiSecuredJwt.Dtos;
using ToDoListApiSecuredJwt.Models;
using ToDoListApiSecuredJwt.Options;

namespace ToDoListApiSecuredJwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPermissionRepo _permissionRepo;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IUserRepo userRepo, IPermissionRepo permissionRepo, IMapper mapper, JwtSettings jwtSettings)
        {
            _userRepo = userRepo;
            _permissionRepo = permissionRepo;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto user)
        {
            var userToCreate = _mapper.Map<User>(user);

            if (_userRepo.GetUserByEmail(user.Email) != null) return BadRequest("User already exists");

            //This is super bad! but it is just for demostration concept
            if (!(user.Role.Equals("user") || user.Role.Equals("admin")))
                return BadRequest("Role just can be user/admin");

            _userRepo.CreateUser(userToCreate);

            var userxPermissions = new List<PermissionXUser>() {
                new PermissionXUser { PermissionId = 1, UserId = userToCreate.Id },
                new PermissionXUser { PermissionId = 2, UserId = userToCreate.Id}
            };
            if (user.Role.Equals("admin"))
                userxPermissions.Add(new PermissionXUser { PermissionId = 3, UserId = userToCreate.Id });

            _permissionRepo.CreatePermissionXUserRelationship(userxPermissions);

            _userRepo.SaveChanges();
            _permissionRepo.SaveChanges();

            var userCreated = _mapper.Map<UserReadDto>(userToCreate);

            return Created("", userCreated);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponseDto> login(LoginRequestDto loginRequest)
        {
            var user = _userRepo.GetUserByEmail(loginRequest.Email);

            if (user == null) return NotFound("User is not registered");
            if (!user.Password.Equals(loginRequest.Password)) return Unauthorized("Email/Password invalid");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor 
            { 
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (var per in user.PermissionsXUser)
                tokenDescriptor.Subject.AddClaim(new Claim(per.Permit.Permit, per.Permit.Id.ToString()));

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new LoginResponseDto { Token = tokenHandler.WriteToken(token) });
        }
    }
}
