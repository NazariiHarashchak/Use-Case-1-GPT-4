using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using Moq;
using Moq.Protected;
using Use_Case_1_GPT_4;
using Use_Case_1_GPT_4.Models;

namespace Use_Case_1_GPT_4_Tests
{
    public class DataProcessorTests
    {
        private readonly DataProccessor _processor;
        private readonly List<Country> _mockData;

        public DataProcessorTests()
        {
            _processor = new DataProccessor();
            _mockData = new List<Country>
            {
                new Country { name = new Name { common = "CountryA", official = "OfficialCountryA" }, population = 1000000 },
                new Country { name = new Name { common = "CountryB", official = "OfficialCountryB" }, population = 2000000 },
                new Country { name = new Name { common = "CountryC", official = "OfficialCountryC" }, population = 500000 }
                // Add more mock data as needed...
        };
        }

        [Fact]
        public void GetCountryByName_ReturnsExpectedCountries()
        {
            //Arrange

            //Act
            var result = _processor.GetCountryByName("CountryA", _mockData).ToList();

            //Assert
            Assert.Single(result);
            Assert.Equal("CountryA", result[0].name?.common);
        }

        [Fact]
        public void GetCountriesByPopulation_ReturnsExpectedCountries()
        {
            //Arrange

            //Act
            var result = _processor.GetCountriesByPopulation(1.5, _mockData).ToList();

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetSortedByName_Ascending_ReturnsSortedCountries()
        {
            //Arrange

            //Act
            var result = _processor.GetSortedByName("ascend", _mockData).ToList();

            //Arrange
            Assert.Equal("CountryA", result[0].name?.common);
        }

        [Fact]
        public void GetSortedByName_Descending_ReturnsSortedCountries()
        {
            //Arrange

            //Act
            var result = _processor.GetSortedByName("descend", _mockData).ToList();

            //Assert
            Assert.Equal("CountryC", result[0].name?.common);
        }

        [Fact]
        public void GetPagination_ReturnsExpectedCountries()
        {
            //Arrange

            //Act
            var result = _processor.GetPagination(2, _mockData).ToList();

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetCountries_ReturnsExpectedCountries()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(_mockData)),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);

            //Act
            var result = await _processor.GetCountries(client);

            //Arrange
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ProccessDate_ReturnsExpectedCountries()
        {
            //Arrange
            var model = new FirstTaskModel { name = "Country", sortByOption = "ascend", population = 1.5, pagesCount = 2 };

            //Act
            var result = _processor.ProccessDate(model, _mockData).ToList();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("CountryA", result[0].name?.common);
        }
    }
}