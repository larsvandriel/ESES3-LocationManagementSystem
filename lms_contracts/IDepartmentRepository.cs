using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Contracts
{
    public interface IDepartmentRepository: IRepositoryBase<Department>
    {
        ShapedEntity GetDepartmentById(Guid departmentId, string fields);
        Department GetDepartmentById(Guid departmentId);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department dbDepartment, Department department);
        void DeleteDepartment(Department department);
    }
}
