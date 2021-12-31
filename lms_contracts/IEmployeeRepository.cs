using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Contracts
{
    public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        ShapedEntity GetEmployeeById(Guid employeeId, string fields);
        Employee GetEmployeeById(Guid employeeId);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee dbEmployee, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
