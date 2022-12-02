using ExampleApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Shared.Interfaces
{
    public interface ILoanApplicationService
    {
        Task<LoanDetailsDTO?> SaveApplication(CustomerApplicationDTO customerApplication, CancellationToken cancellationToken = default);
    }
}
