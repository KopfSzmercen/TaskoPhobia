using System.Net;
using Shouldly;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTests
{
    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }
    
    [Fact]
    public async Task get_home_endpoint_should_return_200_ok_and_text_Hello()
    {
       
        var response = await HttpClient.GetAsync("home");

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();

        content.ShouldNotBeNull();
        content.ShouldBe("Hello");
    }
}