using Microsoft.AspNetCore.Identity;

namespace MyShop.Domain.AspIdentities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            Users = new List<ApplicationUserRole>();
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public IEnumerable<ApplicationUserRole> Users { get; set; }
    }
}
