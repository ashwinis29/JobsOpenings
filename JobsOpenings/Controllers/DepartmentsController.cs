using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobsOpenings.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartments(Models.Department request)
        {
            try
            {
                bool success = _departmentRepository.AddDepartments(request);
                if (success)
                {
                    // Manually construct the location URL
                    var locationUri = $"{Request?.Scheme}://{Request?.Host.ToUriComponent()}/api/v1/departments/{request.Id}";
                    return Created(locationUri, 201);
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
        public async Task<ActionResult> UpdateDepartments(int id, Models.Department request)
        {
            try
            {
                bool success = _departmentRepository.UpdateDepartments(id, request);
                if (success)
                {
                    return Ok();
                }
                else { return BadRequest("Department id not found"); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<Models.Department>> GetDepartments()
        {
            try
            {
                List<Department> departments = _departmentRepository.GetDepartments();
                if (departments != null)
                {
                    return Ok(departments);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
