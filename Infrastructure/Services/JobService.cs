using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
  public class JobService : IJobService
  {
    private readonly IConfiguration _config;

    public JobService(IConfiguration config)
    {
      _config = config;
    }

    public async Task<IReadOnlyList<JobSource>> GetSourceJobs() {
      var baseApiUrl = _config["BaseApiurl"];
      var baseAddress = new Uri(baseApiUrl);

      using (var httpClient = new HttpClient { BaseAddress = baseAddress })
      {
        try
        {
          using (var response = await httpClient.GetAsync("jobs"))
          {
            var responseData = await response.Content.ReadAsStringAsync();
            var jobs = JsonConvert.DeserializeObject<List<JobSource>>(responseData);

            return jobs;
          }
        }
        catch (System.Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }
      }
    }

    public IReadOnlyList<int> GetRelevanceWeightings()
    {
      var key = "SkillsWeightings:JobRelevance";
      var value = _config[key];
      var weightings = value.Split(',').Select(int.Parse).ToList();

      return weightings;
    }
  }
}