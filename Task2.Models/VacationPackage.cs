namespace Task2.Models
{
    public class VacationPackage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int GrantedDays { get; set; }
        public int Year { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
