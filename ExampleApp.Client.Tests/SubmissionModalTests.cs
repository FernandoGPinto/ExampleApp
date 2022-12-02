using Bogus;
using Bunit;
using Bunit.Rendering;
using ExampleApp.Client.Components;
using ExampleApp.Shared.Models;
using System.Collections.Generic;
using Xunit;

namespace ExampleApp.Client.Tests
{
    public class SubmissionModalTests : TestContext
    {

        [Fact]
        public void DefaultRender()
        {
            var cut = SetupComponent();

            Assert.Throws<ComponentNotFoundException>(() => cut.FindComponent<ReadOnlyInput>());

            var p = cut.Find("#nodata");
            p.MarkupMatches("<p id=\"nodata\">No data was returned.</p>");
        }

        [Fact]
        public void RendersWithData()
        {
            var cut = SetupComponent(new LoanDetailsDTO());

            Assert.Throws<ElementNotFoundException>(() => cut.Find("#nodata"));

            var inputs = cut.FindComponents<ReadOnlyInput>();
            Assert.NotNull(inputs);
            Assert.Equal(4, inputs.Count);
        }

        [Theory, MemberData(nameof(GenerateData))]
        public void PassesDataInCorrectly(LoanDetailsDTO data)
        {
            var cut = SetupComponent(data);
            var inputs = cut.FindComponents<ReadOnlyInput>();
            Assert.NotNull(inputs);
            Assert.Collection(inputs, 
                    item => Assert.Equal(data.LoanAmount.ToString(), item.Instance.DisplayValue),
                    item => Assert.Equal(data.TotalAmountPayable.ToString(), item.Instance.DisplayValue),
                    item => Assert.Equal(data.Repayments.ToString(), item.Instance.DisplayValue),
                    item => Assert.Equal(data.APR.ToString(), item.Instance.DisplayValue));
        }

        /// <summary>
        /// Generates fake Loan Details data.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<object[]> GenerateData()
        {
            var loanDetails = new Faker<LoanDetailsDTO>()
                .RuleFor(o => o.APR, f => f.Random.Double(1.00, 100.00))
                .RuleFor(o => o.LoanAmount, f => f.Random.Int(1, 1000))
                .RuleFor(o => o.Repayments, f => f.Random.Double(1.00, 100.00))
                .RuleFor(o => o.TotalAmountPayable, f => f.Random.Double(1.00, 1000.00)).Generate(10);

            foreach (var loanDetail in loanDetails)
            {
                yield return new object[] { loanDetail };
            }
        }

        /// <summary>
        /// Sets up component.
        /// </summary>
        /// <returns></returns>
        private IRenderedComponent<SubmissionModal> SetupComponent()
        {
            return RenderComponent<SubmissionModal>();
        }

        /// <summary>
        /// Sets up component with parameter provided.
        /// </summary>
        /// <param name="loanDetails"></param>
        /// <returns></returns>
        private IRenderedComponent<SubmissionModal> SetupComponent(LoanDetailsDTO? loanDetails)
        {
            return RenderComponent<SubmissionModal>(ps => ps
                    .Add(p => p.LoanDetails, loanDetails));
        }
    }
}