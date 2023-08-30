using System.Diagnostics.Metrics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Use_Case_1_GPT_4.Models;

namespace Use_Case_1_GPT_4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly HttpClient httpClient = new();
        private readonly DataProccessor dataProccessor = new();

        [Route("filter-countries")]
        [HttpPost]
        public async Task<IActionResult> ReceiveValues([FromForm] FirstTaskModel model)
        {
            var data = await dataProccessor.GetCountries(httpClient);
            return Ok(dataProccessor.ProccessDate(model, data));
        }

        [Route("by-name")]
        [HttpGet]
        public async Task<IActionResult> GetCountryByName([FromQuery]string name)
        {
            var data = await dataProccessor.GetCountries(httpClient);
            return Ok(dataProccessor.GetCountryByName(name, data));
        }

        [Route("by-population")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesByPopulation([FromQuery] double population)
        {
            var data = await dataProccessor.GetCountries(httpClient);
            return Ok(dataProccessor.GetCountriesByPopulation(population, data));
        }

        [Route("sort-by/name-common")]
        [HttpGet]
        public async Task<IActionResult> GetSortedByName([FromQuery] string sortByOption)
        {
            var data = await dataProccessor.GetCountries(httpClient);
            return Ok(dataProccessor.GetSortedByName(sortByOption, data));
        }

        [Route("pagination")]
        [HttpGet]
        public async Task<IActionResult> GetPagination([FromQuery] int pagesCount)
        {
            var data = await dataProccessor.GetCountries(httpClient);
            return Ok(dataProccessor.GetPagination(pagesCount, data));
        }
    }
}
