namespace ExampleApp.Server.Services
{
    public static class LoanCalculator
    {
        /// <summary>
        /// Calculates the monthly repayments given the credit amount, period interest rate and number of periods.
        /// </summary>
        /// <param name="loanRequestAmount"></param>
        /// <returns></returns>
        public static double CalculateTotalAmountPayable(int loanRequestAmount, double periodInterestRate, int numberOfPeriods)
        {
            return CalculateRepayments(loanRequestAmount, periodInterestRate, numberOfPeriods) * numberOfPeriods;
        }

        /// <summary>
        /// Calculates the total amount payable given the credit amount, period interest rate and number of periods.
        /// </summary>
        /// <param name="loanRequestAmount"></param>
        /// <returns></returns>
        public static double CalculateRepayments(int loanRequestAmount, double periodInterestRate, int numberOfPeriods)
        {
            return (loanRequestAmount * periodInterestRate) / (1 - Math.Pow((1 + periodInterestRate), -numberOfPeriods));
        }

        /// <summary>
        /// Calculates the APR given the annual interest rate.
        /// </summary>
        /// <param name="annualInterestRate"></param>
        /// <returns></returns>
        public static double CalculateAPR(double annualInterestRate)
        {
            return Math.Pow((1.00 + (annualInterestRate / 12.00)), 12) - 1.00;
        }
    }
}
