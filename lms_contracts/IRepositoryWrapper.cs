using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Contracts
{
    public interface IRepositoryWrapper
    {
        IDepartmentRepository Department { get; }
        IEmployeeRepository Employee { get; }
        ILocationRepository Location { get; }
        IRoomRepository Room { get; }
        void Save();
    }
}
