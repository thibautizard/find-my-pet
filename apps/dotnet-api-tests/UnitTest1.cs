using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace dotnet_api_tests;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;

  public BasicTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task GetAllAnimals_ReturnsOkResult()
  {
    var client = _factory.CreateClient();
    var response = await client.GetAsync("/animals");
    response.EnsureSuccessStatusCode();
  }

  [Fact]
  public async Task GetAnimalById_ReturnsCreatedResult()
  {
    var client = _factory.CreateClient();
    var response = await client.GetAsync("/animals/1");
    response.EnsureSuccessStatusCode();
  }

  [Fact]
  public async Task CreateAnimal_ReturnsCreatedCode()
  {
    var client = _factory.CreateClient();
    var response = await client.PostAsJsonAsync("animals", new Animal { Id = 3, Name = "Scar" });
    response.EnsureSuccessStatusCode();
  }

  [Fact]
  public async Task CreateAnimal_ReturnsBadRequestResult()
  {
    var client = _factory.CreateClient();
    var response = await client.PostAsJsonAsync("/animals", new { });

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }
}
