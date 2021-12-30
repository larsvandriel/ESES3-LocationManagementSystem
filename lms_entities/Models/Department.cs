using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Employee HeadOfDepartment { get; set; }
        public List<Employee> TeamLeads { get; set; }
    }
}
