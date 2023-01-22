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
}