using ExampleApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExampleApp.Shared.Models
{
    public class CustomerApplicationDTO
    {
        [Required(ErrorMessage = "Please enter your full name")]
        [MaxLength(50, ErrorMessage = "The name must be up to 50 characters long")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Please select your title")]
        public Titles? Title { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [MaxLength(100, ErrorMessage = "The name must be up to 100 characters long")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter your annual income")]
        [Range(0, int.MaxValue, ErrorMessage = "Your annual income must be between £0 and £{2}")]
        public int? AnnualIncome { get; set; }

        [Required(ErrorMessage = "Please tell us if you are a home owner")]
        public bool? HomeOwner { get; set; }

        public string? CarRegistration { get; set; }

        [Required(ErrorMessage = "Please enter the loan amount")]
        [Range(1, int.MaxValue, ErrorMessage = "The loan amount requested must be between £1 and £{2}")]
        public int? LoanRequestAmount { get; set; }
    }
}