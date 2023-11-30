using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using static JobsOpenings.Models.JobsModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace JobsOpenings.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly AppDbContext _dataContext;

        public JobsRepository(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddJobs(tblJobs job)
        {
            bool success = false;
            try
            {
                if(job != null)
                {
                    _dataContext.tblJobs.Add(job);
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch(Exception ex) { success = false; }
            return success;
        }

        public bool UpdateJobs(int  jobId, Models.JobsModel.JobOpenings request)
        {
            bool success = false;
            try
            {
                var existingJob = _dataContext.tblJobs.FirstOrDefault(t => t.Id == jobId);
                if (existingJob != null)
                {
                    tblJobs job = DataMapper.jobOpenings(request, existingJob);
                    _dataContext.Entry(job).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex) { success = false; } 
            return success;
        }

        public PageResponse GetJobList(PageRequest pageRequest)
        {
            PageResponse response = new PageResponse();
            try
            {

                var paginatedQuery = pageRequest.PageSize * (pageRequest.PageNo - 1);
                //Fetch all matching records from 3 tables based on ID
                IQueryable<JobsModel.Data> jobList = (from j in _dataContext.tblJobs
                                                join l in _dataContext.Location on j.locationId equals l.Id
                                                join d in _dataContext.Department on j.departmentId equals d.Id
                                                select new JobsModel.Data
                                                {
                                                    Id = j.Id,
                                                    Code = j.code,
                                                    Title = j.title,
                                                    Location = l.title,
                                                    Department = d.title,
                                                    PostedDate = j.postedDate,
                                                    ClosingDate = j.closingDate
                                                }
                                   );
                //if incase both are present then return location id data as 1st priority
                if (pageRequest.LocationId != 0)
                {
                    // Filter by location ID if provided
                    jobList = jobList.Where(job => job.Id == pageRequest.LocationId);
                }
                else if (pageRequest.DepartmentId != 0)
                {
                    // Filter by department ID if provided
                    jobList = jobList.Where(job => job.Id == pageRequest.DepartmentId);
                }
                    //Return data as per pagination
                response.data = jobList.OrderBy(m => m.Id).Skip(paginatedQuery).Take(pageRequest.PageSize).ToList();
 
                response.Total = jobList.Count();
            }
            catch(Exception ex) { }
            return response;
        }

        public JobsModel.Details? GetJobDetails(int id)
        {
            JobsModel.Details? jobList = null;
            try
            {
                jobList = (from j in _dataContext.tblJobs
                                             join l in _dataContext.Location on j.locationId equals l.Id
                                             join d in _dataContext.Department on j.departmentId equals d.Id
                                             where j.Id == id
                                             select new JobsModel.Details
                                             {
                                                 Id = j.Id,
                                                 Code = j.code,
                                                 Title = j.title,
                                                 Description = j.description,
                                                 Location = l,
                                                 Department = d,
                                                 PostedDate = j.postedDate,
                                                 ClosingDate = j.closingDate
                                             }
                               ).FirstOrDefault();
            }
            catch(Exception ex) { return null; }
            return jobList;
            
        }
    }
}
