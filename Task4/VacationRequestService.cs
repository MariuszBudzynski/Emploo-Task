using Task2.Models;
using Task3;

namespace Task4
{
    public class VacationRequestService : IVacationRequestService
    {
        private readonly ICountVacationService _countVacationService;
        public VacationRequestService()
        {
            _countVacationService = new CountVacationService();
        }
        public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
        {
            return _countVacationService.CountFreeDaysForEmployee(employee, vacations, vacationPackage) > 0;
        }
    }
}
