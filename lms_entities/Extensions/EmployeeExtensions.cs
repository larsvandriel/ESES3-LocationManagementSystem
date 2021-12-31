using LocationManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Extensions
{
    public static class EmployeeExtensions
    {
        public static void Map(this Employee dbEmployee, Employee employee)
        {
            dbEmployee.EmployeeNumber = employee.EmployeeNumber;
            dbEmployee.Name = employee.Name;
            dbEmployee.Email = employee.Email;
            dbEmployee.PhoneNumber = employee.PhoneNumber;
            dbEmployee.Address = employee.Address;
        }
    }
}
