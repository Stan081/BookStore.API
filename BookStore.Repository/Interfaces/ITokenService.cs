using System;
using System.Threading.Tasks;
using BookStore.Data.Models;

namespace BookStore.Repository.Interfaces
{
    public interface ITokenService
    {
      Task<string> CreateToken(User user);
    }
}
