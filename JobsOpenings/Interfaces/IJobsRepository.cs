using JobsOpenings.Models;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.Interfaces
{
    public interface IJobsRepository
    {
        bool AddJobs(tblJobs jobs);
        bool UpdateJobs(int jobId, Models.JobsModel.JobOpenings request);
        PageResponse GetJobList(PageRequest pageRequest);
        JobsModel.Details GetJobDetails(int id);
    }
}
