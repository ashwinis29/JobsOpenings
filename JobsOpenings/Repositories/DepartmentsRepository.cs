using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.Repositories
{
    public class DepartmentsRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dataContext;

        public DepartmentsRepository(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddDepartments(Department dept)
        {
            bool success = false;
            try
            {
                if (dept != null)
                {
                    _dataContext.Department.Add(dept);
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex) { success = false; }
            return success;
        }

        public bool UpdateDepartments(int id, Department request)
        {
            bool success = false;
            try
            {
                var existingDepartment = _dataContext.Department.FirstOrDefault(t => t.Id == id);
                if (existingDepartment != null)
                {
                    existingDepartment.title = request.title;
                    _dataContext.Entry(existingDepartment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex) { success = false; }
            return success;
        }

        public List<Models.Department> GetDepartments()
        {
            List<Department> department = null;
            try
            {
              department = _dataContext.Department.ToList();
            }
            catch (Exception ex) { return department; }
            return department;
        }
    }
}
