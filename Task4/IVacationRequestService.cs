using Task2.Models;

namespace Task4
{
    public interface IVacationRequestService
    {
        bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage);
    }
}