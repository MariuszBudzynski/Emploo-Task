using Task2.Models;
using Task4;

namespace Task5.Tests
{
    public class VacationServiceTests
    {
        private readonly IVacationRequestService _sut;
        public VacationServiceTests()
        {
            _sut = new VacationRequestService();
        }

        [Fact]
        public void employee_can_request_vacation()
        {
            //Arrange
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

            //Act
            var result = _sut.IfEmployeeCanRequestVacation(employee, vacations, package);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void employee_cant_request_vacation()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "Jan" };
            var package = new VacationPackage { Id = 1, GrantedDays = 2, Year = DateTime.Today.Year };

            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    EmployeeId = 1,
                    DateSince = DateTime.Today.AddDays(-5),
                    DateUntil = DateTime.Today.AddDays(-4),
                    IsPartialVacation = false
                }
            };

            //Act
            var result = _sut.IfEmployeeCanRequestVacation(employee, vacations, package);

            //Assert
            Assert.False(result);
        }
    }
}
