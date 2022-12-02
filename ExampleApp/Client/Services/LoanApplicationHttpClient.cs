using ExampleApp.Shared.Interfaces;
using ExampleApp.Shared.Models;
using System.Net.Http.Json;

namespace ExampleApp.Client.Services
{
    public class LoanApplicationHttpClient : ILoanApplicationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoanApplicationHttpClient> _logger;

        public LoanApplicationHttpClient(HttpClient httpClient, ILogger<LoanApplicationHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Sends request to the server to save the customer application.
        /// </summary>
        /// <param name="customerApplication"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<LoanDetailsDTO?> SaveApplication(CustomerApplicationDTO customerApplication, CancellationToken cancellationToken = default)
        {
            LoanDetailsDTO? loanDetails = null;
            try
            {
                var response = await _httpClient.PostAsJsonAsync("CustomerApplication/Post", customerApplication, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    loanDetails = response.Content.ReadFromJsonAsync<LoanDetailsDTO>(cancellationToken: cancellationToken).Result!;
                }
            }
            catch (TaskCanceledException e)
            {
                _logger.LogError(e, "Submission was canceller by the user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to POST a customer loan application");
            }

            return loanDetails;
        }
    }
}
