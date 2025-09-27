using Task1.Models;

namespace Task1
{
    public class EmployeeHierarchy : IEmployeeHierarchy
    {
        public List<EmployeesStructure> FillEmployeesStructure(List<Employee> employees)
        {
            if (employees == null)
                throw new ArgumentNullException(nameof(employees));

            var result = new List<EmployeesStructure>();
            //used to check for duplications
            var existingRelations = new HashSet<(int EmployeeId, int SuperiorId)>();

            foreach (var e in employees)
            {
                ProcessEmployee(e, employees, result, existingRelations);
            }

            return result;
        }

        public int? GetSuperiorRowOfEmployee(int employeeId, int superiorId, List<EmployeesStructure> structure)
        {
            var relation = structure.Find(x => x.EmployeeId == employeeId && x.SuperiorId == superiorId);
            return relation?.Level;
        }

        //helper methods
        private void ProcessEmployee(
        Employee employee,
        List<Employee> employees,
        List<EmployeesStructure> result,
        HashSet<(int, int)> existingRelations)
        {
            int level = 0;
            var current = employee;

            // used to detect cycles
            var visited = new HashSet<int>(); 

            while (current.SuperiorId.HasValue)
            {
                level++;
                var superiorId = current.SuperiorId.Value;

                EnsureNoCycle(visited, superiorId, employee.Id);

                AddRelationIfNotExists(employee.Id, superiorId, level, result, existingRelations);

                // try to move up the chain
                var next = employees.Find(x => x.Id == superiorId);

                // superior not found in the list, stop
                if (next == null)
                    break; 

                current = next;
            }
        }

        private void EnsureNoCycle(HashSet<int> visited, int superiorId, int employeeId)
        {
            // detect cycle in hierarchy (e.g., A -> B -> A)
            if (visited.Contains(superiorId))
                throw new InvalidOperationException($"Cycle detected for Employee {employeeId}");

            visited.Add(superiorId);
        }

        private void AddRelationIfNotExists(
        int employeeId,
        int superiorId,
        int level,
        List<EmployeesStructure> result,
        HashSet<(int, int)> existingRelations)
        {
            var key = (employeeId, superiorId);
            if (!existingRelations.Contains(key))
            {
                result.Add(new EmployeesStructure
                {
                    EmployeeId = employeeId,
                    SuperiorId = superiorId,
                    Level = level
                });

                existingRelations.Add(key);
            }
        }

    }
}
