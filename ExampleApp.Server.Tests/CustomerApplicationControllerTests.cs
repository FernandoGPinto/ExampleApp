using ExampleApp.Server.Controllers;
using ExampleApp.Shared.Interfaces;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExampleApp.Server.Tests
{
    public class CustomerApplicationControllerTests
    {
        private readonly Mock<ILogger<CustomerApplicationController>> _mockLogger;
        private readonly Mock<ILoanApplicationService> _loanApplicationService;

        private readonly CustomerApplicationController _controller;

        public CustomerApplicationControllerTests()
        {
            _mockLogger = new Mock<ILogger<CustomerApplicationController>>();
            _loanApplicationService = new Mock<ILoanApplicationService>();

            _controller = new(_loanApplicationService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Post_NullModel_ReturnsBadRequest()
        {
            var result = await _controller.Post(null!);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Post_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("FullName", "FullName is required");

            var result = await _controller.Post(new CustomerApplicationDTO());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Post_ServiceError_Returns500Error()
        {
            _loanApplicationService.Setup(x => x.SaveApplication(It.IsAny<CustomerApplicationDTO>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((LoanDetailsDTO?)null);

            var result = await _controller.Post(new CustomerApplicationDTO());

            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult?.StatusCode);
        }

        [Fact]
        public async Task Post_ModelStateValid_ReturnsOk()
        {
            var loanAmount = 10;
            _loanApplicationService.Setup(x => x.SaveApplication(It.IsAny<CustomerApplicationDTO>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new LoanDetailsDTO() { LoanAmount = loanAmount });

            var result = await _controller.Post(new CustomerApplicationDTO());

            Assert.IsType<OkObjectResult>(result);
            var loanDetails = (result as OkObjectResult).Value as LoanDetailsDTO;

            Assert.Equal(loanAmount, loanDetails.LoanAmount);
        }
    }
}