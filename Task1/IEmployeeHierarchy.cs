using Task1.Models;

namespace Task1
{
    public interface IEmployeeHierarchy
    {
        List<EmployeesStructure> FillEmployeesStructure(List<Employee> employees);
        int? GetSuperiorRowOfEmployee(int employeeId, int superiorId, List<EmployeesStructure> structure);
    }
}