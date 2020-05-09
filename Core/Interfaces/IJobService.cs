using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IJobService
  {
    IReadOnlyList<int> GetRelevanceWeightings();

    Task<IReadOnlyList<JobSource>> GetSourceJobs();
  }
}