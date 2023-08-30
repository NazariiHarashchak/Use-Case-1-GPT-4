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
        [Route("task-1")]
        [HttpPost]
        public async Task<IActionResult> ReceiveValues([FromForm] FirstTaskModel model)
        {
            var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<object>>(content);
                Console.WriteLine(content);
            }
            return Ok(true);
        }
    }
}
