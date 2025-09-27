using Task2.Models;

namespace Task3.Tests
{
    public class CountVacationServiceTests
    {
        private readonly ICountVacationService _sut;

        public CountVacationServiceTests()
        {
            _sut = new CountVacationService();
        }

        [Fact]
        public void returns_full_granted_days_when_no_vacations()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 10, Year = DateTime.Today.Year };
            var vacations = new List<Vacation>();

            // Act
            var result = _sut.CountFreeDaysForEmployee(employee, vacations, package);

            // Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void subtracts_regular_vacation_days()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 10, Year = DateTime.Today.Year };

            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    EmployeeId = 1,
                    DateSince = DateTime.Today.AddDays(-5),
                    DateUntil = DateTime.Today.AddDays(-3),
                    IsPartialVacation = false
                }
            };

            // Act
            var result = _sut.CountFreeDaysForEmployee(employee, vacations, package);

            // Assert
            // 3 days vacation (DateUntil - DateSince = 2 + 1 = 3)
            Assert.Equal(7, result);
        }

        [Fact]
        public void subtracts_partial_vacation_days()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 10, Year = DateTime.Today.Year };

            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    EmployeeId = 1,
                    DateSince = DateTime.Today.AddDays(-5),
                    DateUntil = DateTime.Today.AddDays(-5),
                    IsPartialVacation = true,
                    NumberOfHours = 16 // 2 days
                }
            };

            // Act
            var result = _sut.CountFreeDaysForEmployee(employee, vacations, package);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void returns_zero_when_used_days_exceed_granted_days()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 2, Year = DateTime.Today.Year };

            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    EmployeeId = 1,
                    DateSince = DateTime.Today.AddDays(-10),
                    DateUntil = DateTime.Today.AddDays(-5),
                    IsPartialVacation = false
                }
            };

            // Act
            var result = _sut.CountFreeDaysForEmployee(employee, vacations, package);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void throws_when_invalid_dates()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 10, Year = DateTime.Today.Year };

            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    EmployeeId = 1,
                    DateSince = DateTime.Today,
                    DateUntil = DateTime.Today.AddDays(-1), // invalid: until before since
                    IsPartialVacation = false
                }
            };

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() =>
                _sut.CountFreeDaysForEmployee(employee, vacations, package));
        }
    }
}