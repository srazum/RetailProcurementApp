using Bogus.Bson;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RetailProcurement.WebAPI.Dtos;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RetailProcurement.UnitTests;
public class BasicTests
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string? _username;
    private readonly string? _password;

    private readonly string? SUPPLIER_NAME = "testSupplier1";
    private readonly string? UPDATED_SUPPLIER_NAME = "testSupplier2";
    public BasicTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.ConfigureClient();
    }
    [Theory]
    [InlineData("store-items")]
    [InlineData("suppliers")]
    [InlineData("orders")]
    [InlineData("supplier-store-items")]
    [InlineData("statistics/quarterly-plan")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string apiSubject)
    {
        var response = await _client.GetAsync($"api/{apiSubject}");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType!.ToString());
    }

    [Fact]
    public async Task GetById_EndpointsReturnSuccessAndCorrectContentType()
    {
        var response = await _client.GetAsync($"api/suppliers");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType!.ToString());
    }

    [Fact]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType()
    {
        var supplier = new SupplierDto { Name = SUPPLIER_NAME };
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(supplier)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _client.PostAsync($"api/suppliers", byteContent);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Put_EndpointsReturnSuccessAndCorrectContentType()
    {
        var supplier = new SupplierDto { Name = SUPPLIER_NAME };
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(supplier)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _client.PostAsync($"api/suppliers", byteContent);
        var supplier2 = await response.Content.ReadFromJsonAsync<SupplierDto>();
        supplier2!.Name = UPDATED_SUPPLIER_NAME;
        var byteContent2 = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(supplier2)));
        byteContent2.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response2 = await _client.PutAsync($"api/suppliers/{supplier2.Id}", byteContent2);
        response2.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_EndpointsReturnSuccessAndCorrectContentType()
    {
        var supplier = new SupplierDto { Name = SUPPLIER_NAME };
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(supplier)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _client.PostAsync($"api/suppliers", byteContent);
        var supplier2 = await response.Content.ReadFromJsonAsync<SupplierDto>();
        supplier2!.Name = UPDATED_SUPPLIER_NAME;
        var byteContent2 = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(supplier2)));
        byteContent2.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response2 = await _client.DeleteAsync($"api/suppliers/{supplier2.Id}");
        response2.EnsureSuccessStatusCode();
    }

}