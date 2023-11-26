using static JobsOpenings.Models.JobsModel;
using System.Reflection.Metadata;
using JobsOpenings.Models;

namespace JobsOpenings.Data
{
    public static class DataMapper
    {
        public static tblJobs jobOpenings(JobOpenings jobs, tblJobs job)
        {
            if (jobs != null)
            {
                job.title = jobs.Title;
                job.description = jobs.Description;
                job.locationId = jobs.LocationId;
                job.departmentId = jobs.DepartmentId;
                job.postedDate = DateTime.Now;
                job.closingDate = jobs.ClosingDate;
            }

            return job;
        }

        public static Location location(Location location, Location response)
        {

            if(location != null)
            {
                response.Id = location.Id;
                response.title = location.title;
                response.city = location.city;
                response.country = location.country;
                response.state = location.state;
                response.zip = location.zip;
            }
            return response;
        }
    }
}
