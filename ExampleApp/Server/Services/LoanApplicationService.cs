using AutoMapper;
using ExampleApp.Server.Data;
using ExampleApp.Shared.Interfaces;
using ExampleApp.Shared.Models;

namespace ExampleApp.Server.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly IMapper _mapper;
        private readonly LoanDbContext _context;
        private const int NumberOfPeriods = 18;
        private const double MonthlyInterestRate = 0.173;
        private const double AnnualInterestRate = 0.173 * 12.00;

        public LoanApplicationService(IMapper mapper, LoanDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Inserts customer application into database.
        /// </summary>
        /// <param name="customerApplicationDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<LoanDetailsDTO?> SaveApplication(CustomerApplicationDTO customerApplicationDto, CancellationToken cancellationToken = default)
        {
            LoanDetailsDTO? loanDetails = null;

            // Map the CustomerApplicationDTO object to a CustomerApplication object accepted by the db context
            var customerApplication = _mapper.Map<CustomerApplication>(customerApplicationDto);

            customerApplication.Id = Guid.NewGuid();
            customerApplication.CreatedTimestamp = DateTime.UtcNow;

            _context.Add(customerApplication);
            var rows = await _context.SaveChangesAsync(cancellationToken);

            // If application was saved then generate and return loan details
            // Loan conditions are 18-month term and 17.3% monthly interest rate
            if (rows > 0)
            {
                var loanRequestAmount = customerApplication.LoanRequestAmount;
                var totalAmountPayable = string.Concat("£", LoanCalculator.CalculateTotalAmountPayable(loanRequestAmount, MonthlyInterestRate, NumberOfPeriods).ToString("F"));
                var repayments = string.Concat(NumberOfPeriods, "x £", LoanCalculator.CalculateRepayments(loanRequestAmount, MonthlyInterestRate, NumberOfPeriods).ToString("F"));
                var apr = string.Concat((LoanCalculator.CalculateAPR(AnnualInterestRate) * 100.00).ToString("F"), "%");

                loanDetails = new()
                {
                    LoanAmount = string.Concat("£", loanRequestAmount),
                    TotalAmountPayable = totalAmountPayable,
                    Repayments = repayments,
                    APR = apr
                };
            }

            return loanDetails;
        }
    }
}
