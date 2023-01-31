using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Json;
using Domain.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace MinimalAPIDemoTests;

public class ThrowsControllerTests
{
    private readonly HttpClient _api;

    public ThrowsControllerTests()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(
                builder => { builder.UseEnvironment("test"); }
            );

        _api = factory.CreateClient();
    }

    [Fact]
    public async void Controller_throws_error_returns_500()
    {
        //Arrange
        
        //Act
        var response = await _api.GetAsync("/api/throws");
        string content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError,  response.StatusCode);
    }

    [Fact]
    public async void Controller_Returns_Query_Params()
    {

        string Query = "Hello Query";
        int value = 42;
            //Arrange
        var query = new Dictionary<string, string?>()
        {
            ["queryString"] = Query,
            ["queryInt"] = value.ToString(),
        };

        var uri = QueryHelpers.AddQueryString("/api/query", query);

        var response = await _api.GetAsync(uri);
        var content = await response.Content.ReadFromJsonAsync<QueryModel>();
        
        Assert.Equal(Query, content.Query);
        Assert.Equal(value, content.Value);

    }
}