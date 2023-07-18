using Microsoft.AspNetCore.Identity;

namespace MyShop.Domain.AspIdentities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Roles = new List<ApplicationUserRole>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string NationalCode { get; set; }

        public IEnumerable<ApplicationUserRole> Roles { get; set; }
    }
}