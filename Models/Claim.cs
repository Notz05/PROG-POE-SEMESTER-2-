using System;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer Name is required.")]
        [StringLength(100, ErrorMessage = "Lecturer Name can't be longer than 100 characters.")]
        public string LecturerName { get; set; }

        [Required(ErrorMessage = "Claim Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters.")]
        public string Status { get; set; }

        [StringLength(255, ErrorMessage = "Supporting Document Path can't be longer than 255 characters.")]
        public string SupportingDocumentPath { get; set; }

        // Foreign key for User
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        // Navigation property to the User
        public User User { get; set; }
    }
}
