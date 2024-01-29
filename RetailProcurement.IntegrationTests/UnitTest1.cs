using System.Net;

namespace RetailProcurement.IntegrationTests;
public class BasicTests
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BasicTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("http://localhost:5000");
    }

    [Theory]
    [InlineData("/store-items")]
    [InlineData("/suppliers")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string apiSubject)
    {
        // Act
        var response = await _client.GetAsync($"api/{apiSubject}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType!.ToString());
    }
}