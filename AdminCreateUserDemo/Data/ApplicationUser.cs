using Microsoft.AspNetCore.Identity;

namespace AdminCreateUserDemo.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool FirstLogin { get; set; } = true;
    }
}
