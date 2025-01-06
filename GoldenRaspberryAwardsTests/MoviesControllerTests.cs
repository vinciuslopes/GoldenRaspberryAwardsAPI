using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;


namespace GoldenRaspberryAwardsTests
{
    public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _awardIntervalsEndPoint = "/api/movies/award-intervals";

        public MoviesControllerTests(WebApplicationFactory<Program> factory)
        {
            // Cria um cliente HTTP que simula as chamadas à API.
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAwardIntervals_ShouldReturnSuccess()
        {
            // Faz uma requisição GET ao endpoint.
            var response = await _client.GetAsync(_awardIntervalsEndPoint);

            // Verifica se a resposta é um código de sucesso (200 OK).
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAwardIntervals_ShouldReturnCorrectData()
        {
            var response = await _client.GetAsync(_awardIntervalsEndPoint);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("min").And.Contain("max");
        }
    }
}
