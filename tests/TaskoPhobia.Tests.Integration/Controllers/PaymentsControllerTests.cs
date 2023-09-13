using System.Net;
using System.Net.Http.Json;
using Shouldly;
using TaskoPhobia.Api.Controllers.Payments.Requests;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class PaymentsControllerTests : ControllerTests
{
    #region Setup

    public PaymentsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider, new Clock())
    {
    }

    #endregion

    [Fact]
    public async Task GivenValidPaymentId_PostPaymentsPaymentId_ShouldReturn200OKHttpStatus()
    {
        var (customer, order) = await CreateOrderAsync();

        var payment = Payment.New(Guid.NewGuid(), order.Id, order.Price.Copy(), DateTimeOffset.Now, "http://url.pl");

        await _testDatabase.WriteDbContext.Payments.AddAsync(payment);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        var request = new PaymentData
        {
            PaymentStatus = PaymentStatus.Pending()
        };


        Authorize(customer.Id, customer.Role);

        var response = await HttpClient.PostAsJsonAsync($"/payments/{payment.Id.Value}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}