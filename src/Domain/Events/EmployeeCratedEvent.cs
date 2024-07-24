using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class EmployeeCratedEvent:BaseEvent
    {
        public Employee Employee { get; }
        public EmployeeCratedEvent(Employee employee) { Employee = employee; }
    }
}
