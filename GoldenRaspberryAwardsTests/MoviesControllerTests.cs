using FluentAssertions;
using GoldenRaspberryAPI.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;


namespace GoldenRaspberryAwardsTests
{
    public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _awardIntervalsEndPoint = "/api/movies/award-intervals";
        private JsonSerializerOptions _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };

        public MoviesControllerTests(WebApplicationFactory<Program> factory)
        {
            // Arrange
            // Cria um cliente HTTP que simula as chamadas à API.
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAwardIntervals_ShouldReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync(_awardIntervalsEndPoint);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InvalidEndpoint_ShouldReturnStatus404()
        {
            // Act
            var response = await _client.GetAsync("/api/movies/invalid-endpoint");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAwardIntervals_ShouldReturnCorrectData()
        {
            // Act
            var response = await _client.GetAsync(_awardIntervalsEndPoint);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("min").And.Contain("max");

            Assert.NotNull(content);
        }

        [Fact]
        public async Task GetAllMovies_ShouldReturnDataFromDatabase()
        {
            // Act
            var response = await _client.GetFromJsonAsync<List<Movie>>("/api/movies/all-movies");

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Count > 0);
            Assert.Contains(response, m => m.Title == "Windows");
        }

        [Fact]
        public async Task GetProducerWithMaxInterval_ShouldReturnCorrectData()
        {
            // Act
            var response = await _client.GetAsync(_awardIntervalsEndPoint);
            var producers = await response.Content.ReadAsStringAsync();
            
            var result = JsonSerializer.Deserialize<AwardIntervalsResponse>(producers, _options);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Max.Count > 0);
            Assert.Equal(6, result.Max.First().Interval);
        }
    }
}
