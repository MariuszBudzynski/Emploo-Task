using Task2.Models;

namespace Task3
{
    public class CountVacationService : ICountVacationService
    {
        public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
        {
            ValidateInputs(employee, vacations, vacationPackage);

            var year = vacationPackage.Year;
            var today = DateTime.Today;

            var usedDays = vacations
                .Where(v => IsVacationValidForYear(v, employee.Id, year, today))
                .Sum(v => CalculateVacationDays(v, employee.Id));

            return CalculateRemainingDays(vacationPackage.GrantedDays, usedDays);
        }

        // ------------------ PRIVATE HELPERS ------------------

        private void ValidateInputs(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (vacations == null)
                throw new ArgumentNullException(nameof(vacations));

            if (vacationPackage == null)
                throw new ArgumentNullException(nameof(vacationPackage));
        }

        private bool IsVacationValidForYear(Vacation vacation, int employeeId, int year, DateTime today)
        {
            return vacation.EmployeeId == employeeId
                && vacation.DateSince.Year == year
                && vacation.DateUntil.Year == year
                && vacation.DateUntil < today;
        }

        private double CalculateVacationDays(Vacation vacation, int employeeId)
        {
            if (vacation.DateUntil < vacation.DateSince)
                throw new InvalidOperationException($"Invalid vacation dates for Employee {employeeId}");

            return vacation.IsPartialVacation
                ? vacation.NumberOfHours / 8.0
                : (vacation.DateUntil - vacation.DateSince).Days + 1;
        }

        private int CalculateRemainingDays(int grantedDays, double usedDays)
        {
            var remaining = grantedDays - (int)usedDays;
            return remaining < 0 ? 0 : remaining;
        }
    }
}