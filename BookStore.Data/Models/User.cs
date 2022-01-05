using Microsoft.AspNetCore.Identity;

namespace BookStore.Data.Models

{
    public class User : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
    }
}
