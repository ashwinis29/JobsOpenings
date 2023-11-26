using JobsOpenings.Models;

namespace JobsOpenings.Interfaces
{
    public interface IDepartmentRepository
    {
        bool AddDepartments(Department dept);
        bool UpdateDepartments(int id, Department request);
        List<Models.Department> GetDepartments();
    }
}
