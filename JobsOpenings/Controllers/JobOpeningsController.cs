using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using JobsOpenings.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobOpeningsController : ControllerBase
    {
        private readonly IJobsRepository _jobsRepository;
        public JobOpeningsController(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateJobs(Models.JobsModel.JobOpenings request)
        {
            try
            {
                Random rd = new Random();
                tblJobs response = new tblJobs();

                tblJobs job = DataMapper.jobOpenings(request, response);
                // Auto generate job id for new entries
                job.code = "JOB-" + rd.Next(1, 1500);
                var id = job.code.Split('-');
                job.Id = Int32.Parse(id[1]);

                bool success = _jobsRepository.AddJobs(job);

                if (success)
                {
                    // Manually construct the location URL
                    var locationUri = $"{Request?.Scheme}://{Request?.Host.ToUriComponent()}/api/v1/jobs/{job.Id}";
                    return Created(locationUri, HttpStatusCode.Created);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJobs(int id, Models.JobsModel.JobOpenings request)
        {
            try
            {
                bool success = _jobsRepository.UpdateJobs(id, request);
                if (success) { return Ok(); }
                else { return BadRequest("Job id not found"); }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<PageResponse>> ListJobs(PageRequest pageRequest)
        {
            try
            {
                PageResponse response = _jobsRepository.GetJobList(pageRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobsModel.Details>> JobsDetails(int id)
        {
            try
            {
                JobsModel.Details jobList = _jobsRepository.GetJobDetails(id);
                if(jobList != null) 
                {
                    return Ok(jobList);
                }
                else
                {
                    return NotFound("Job details not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }


    }
}
