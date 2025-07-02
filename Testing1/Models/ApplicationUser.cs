using Microsoft.AspNetCore.Identity;

namespace Testing1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }  
        public int Age { get; set; }          
    }
}
