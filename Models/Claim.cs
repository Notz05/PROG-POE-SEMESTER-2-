using System; 
namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public string SupportingDocumentPath { get; set; }
        public string Status { get; set; }
    }
}
