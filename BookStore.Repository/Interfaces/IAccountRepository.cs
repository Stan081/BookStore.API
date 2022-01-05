using System;
using System.Threading.Tasks;
using BookStore.Data.Dtos;
using BookStore.Repository.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserDto> SignUpAsync(SignUpDto signUpDto);
        Task<UserDto> AdminSignUpAsync(SignUpDto signUpDto);
        Task<UserDto> LoginAsync(SignInDto signInDto);
    }
}
