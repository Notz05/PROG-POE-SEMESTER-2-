using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimViewModel
    {
        public IEnumerable<Claim> Claims { get; set; } // For the index view
        public Claim ClaimDetails { get; set; } // For the details view
        public string SearchString { get; set; } // For the search feature
        public string SuccessMessage { get; set; } // For success message display
    }
}
