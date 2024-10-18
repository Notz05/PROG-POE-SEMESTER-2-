using System;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }

        public string LecturerName { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public decimal HourlyRate { get; set; } // New property
        public int HoursWorked { get; set; } // New property
        public string SupportingDocumentPath { get; set; }
        public string Status { get; set; }

        // Foreign key for User
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        // Navigation property to the User
        public User User { get; set; }
    }
}
