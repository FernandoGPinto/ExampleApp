using ExampleApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Shared.Models
{
    public class CustomerApplication
    {
        [Key]
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public Titles Title { get; set; }

        public string Address { get; set; }

        public int AnnualIncome { get; set; }

        public bool HomeOwner { get; set; }

        public string? CarRegistration { get; set; }

        public int LoanRequestAmount { get; set; }

        public ApplicationStatus Status { get; set; }

        public DateTime CreatedTimestamp { get; set; }
    }
}
