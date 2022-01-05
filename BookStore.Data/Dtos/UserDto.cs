using System;
using System.Collections.Generic;

namespace BookStore.Data.Dtos
{
    public class UserDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
        public List<string> Errors { get; set; }
    }
}


