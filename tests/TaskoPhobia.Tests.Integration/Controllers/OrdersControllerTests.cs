using System.Net;
using System.Net.Http.Json;
using Shouldly;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Payments;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class OrdersControllerTests : ControllerTests
{
    public OrdersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider, new Clock())
    {
    }

    [Fact]
    public async Task GivenUserHasPlacedOrders_BrowseOrdersShouldReturnListOfOrders()
    {
        var (customer, _) = await CreateOrderAsync();
        Authorize(customer.Id, customer.Role);

        var response = await HttpClient.GetAsync("/orders");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var results = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();

        results.Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GivenUserHasPlacedAnOrder_PostOrderPaymentsShouldReturnPaymentUrl()
    {
        var (customer, order) = await CreateOrderAsync();
        Authorize(customer.Id, customer.Role);

        var response = await HttpClient.PostAsJsonAsync($"/orders/{order.Id.Value.ToString()}/payments", new object());

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<PaymentLinkDto>();
        result.ShouldNotBeNull();

        result?.PaymentLink?.Value.ShouldNotBeNull();
    }
}