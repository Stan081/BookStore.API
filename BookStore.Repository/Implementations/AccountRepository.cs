using System.Threading.Tasks;
using AutoMapper;
using BookStore.Data.Dtos;
using BookStore.Data.Models;
using BookStore.Repository.Dtos;
using BookStore.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
//using System.Collections;

namespace BookStore.Repository.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountRepository(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager,RoleManager<IdentityRole>roleManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
           // _configuration = configuration;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserDto> SignUpAsync(SignUpDto signUpDto)
        {
            var user = _mapper.Map<User>(signUpDto);
            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            //var errorList = new List();
            if (!result.Succeeded) {
                var errorList = result.Errors.Select(e => e.Description).ToList();
                return new UserDto
                {
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    Errors = errorList

                };
            }
        
            var roleResult = await _userManager.AddToRolesAsync(user, new[] { "Customer" });

            if (!roleResult.Succeeded) return null;

            return new UserDto
            {
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Role = await _userManager.GetRolesAsync(user),

            };

            
        }

        public async Task<UserDto> AdminSignUpAsync(SignUpDto signUpDto)
        {
            var user = _mapper.Map<User>(signUpDto);
            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded) return null;

            var roleResult = await _userManager.AddToRolesAsync(user, new[] {"Admin"});

            if (!roleResult.Succeeded) return null;

            return new UserDto
            {
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Role = await _userManager.GetRolesAsync(user)
            };


        }

        public async Task<UserDto> LoginAsync(SignInDto signInDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == signInDto.Email);
            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return new UserDto
            {
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Role = await _userManager.GetRolesAsync(user) 
            };
                
        }

    }
}
