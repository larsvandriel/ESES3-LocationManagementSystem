using LocationManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Extensions
{
    public static class DepartmentExtensions
    {
        public static void Map(this Department dbDepartment, Department department)
        {
            dbDepartment.Name = department.Name;
            dbDepartment.Email = department.Email;
            dbDepartment.PhoneNumber = department.PhoneNumber;
            dbDepartment.HeadOfDepartment = department.HeadOfDepartment;
            dbDepartment.TeamLeads = department.TeamLeads;
        }
    }
}
