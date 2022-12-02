using ExampleApp.Shared.Interfaces;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _applicationService;
        private readonly ILogger<CustomerApplicationController> _logger;

        public CustomerApplicationController(ILoanApplicationService applicationService, ILogger<CustomerApplicationController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]CustomerApplicationDTO customerApplication, CancellationToken cancellationToken = default)
        {
            if (customerApplication is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await _applicationService.SaveApplication(customerApplication, cancellationToken);
                return result != null ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}