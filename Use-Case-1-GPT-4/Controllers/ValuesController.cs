﻿using System.Diagnostics.Metrics;
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
        [Route("task-1")]
        [HttpPost]
        public async Task<IActionResult> ReceiveValues([FromForm] FirstTaskModel model)
        {
            return Ok(true);
        }

        [Route("by-name")]
        [HttpGet]
        public async Task<IActionResult> GetCountryByName([FromQuery]string name)
        {
            var countries = await this.GetCountries();
            var filtered = countries
                .Where(i => i.name.common.ToLower().Contains(name.ToLower())
                        || i.name.official.ToLower().Contains(name.ToLower()));

            return Ok(filtered);
        }

        [Route("by-population")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesByPopulation([FromQuery] double population)
        {
            var countries = await this.GetCountries();
            var filtered = countries
                .Where(i => i.population != null 
                        && i.population <= population * 100000);

            return Ok(filtered);
        }

        [Route("sort-by/name-common")]
        [HttpGet]
        public async Task<IActionResult> GetSortedByName([FromQuery] string sortByOption)
        {
            var countries = await this.GetCountries();

            switch (sortByOption.ToLower())
            {
                case "ascend":
                    var sorted = countries
                        .OrderBy(i => i.name?.common);
                    return Ok(sorted);
                case "descend":
                    var sortedDesc = countries
                        .OrderByDescending(i => i.name?.common);
                    return Ok(sortedDesc);
                default:
                    throw new Exception("Incorrect value provided!");
            }
        }

        [Route("pagination")]
        [HttpGet]
        public async Task<IActionResult> GetPagination([FromQuery] int pagesCount)
        {
            var countries = await this.GetCountries();

            return Ok(countries.Take(pagesCount));
        }

        private async Task<List<Country>> GetCountries()
        {
            var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(content);
                return countries;
            }
            else
            {
                return new List<Country>();
            }
        }
    }
}
