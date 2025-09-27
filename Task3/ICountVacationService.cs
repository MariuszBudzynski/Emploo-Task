using Task2.Models;

namespace Task3
{
    public interface ICountVacationService
    {
        int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage);
    }
}