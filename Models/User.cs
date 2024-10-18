using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Navigation property for Claims
        public ICollection<Claim> Claims { get; set; } // A user can have many claims
    }
}
