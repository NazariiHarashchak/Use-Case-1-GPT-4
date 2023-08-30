using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Use_Case_1_GPT_4.Models;

namespace Use_Case_1_GPT_4
{
    public class DataProccessor
    {
        public IEnumerable<Country> ProccessDate(FirstTaskModel model, IEnumerable<Country> data)
        {
            return GetPagination(model.pagesCount ?? 1,
                        GetSortedByName(model.sortByOption ?? "ascend",
                            GetCountriesByPopulation(model.population ?? 0,
                                GetCountryByName(model.name ?? "", data))));
        }

        public IEnumerable<Country> GetCountryByName(string name, IEnumerable<Country> data)
        {
            var filtered = data
                .Where(i => i.name.common.ToLower().Contains(name.ToLower())
                        || i.name.official.ToLower().Contains(name.ToLower()));

            return filtered;
        }

        public IEnumerable<Country> GetCountriesByPopulation(double population, IEnumerable<Country> data)
        {
            var filtered = data
                .Where(i => i.population != null
                        && i.population <= population * 1000000);

            return filtered;
        }

        public IEnumerable<Country> GetSortedByName(string sortByOption, IEnumerable<Country> data)
        {
            switch (sortByOption.ToLower())
            {
                case "ascend":
                    var sorted = data
                        .OrderBy(i => i.name?.common);
                    return sorted;
                case "descend":
                    var sortedDesc = data
                        .OrderByDescending(i => i.name?.common);
                    return sortedDesc;
                default:
                    throw new Exception("Incorrect value provided!");
            }
        }

        public IEnumerable<Country> GetPagination(int pagesCount, IEnumerable<Country> data)
        {
            return data.Take(pagesCount);
        }

        public async Task<IEnumerable<Country>> GetCountries(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var countires = JsonSerializer.Deserialize<IEnumerable<Country>>(content);
                return countires;
            }
            else
            {
                return new List<Country>();
            }
        }
    }
}
