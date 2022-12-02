using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Shared.Models
{
    public class LoanDetailsDTO
    {
        public string LoanAmount { get; set; }

        public string TotalAmountPayable { get; set; }

        public string Repayments { get; set; }

        public string APR { get; set; }
    }
}
