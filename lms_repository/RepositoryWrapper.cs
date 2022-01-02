using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities;
using LocationManagementSystem.Entities.Helpers;
using LocationManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;

        private IDepartmentRepository _department;
        private ISortHelper<Department> _departmentSortHelper;
        private IDataShaper<Department> _departmentDataShaper;

        private IEmployeeRepository _employee;
        private ISortHelper<Employee> _employeeSortHelper;
        private IDataShaper<Employee> _employeeDataShaper;

        private ILocationRepository _location;
        private ISortHelper<Location> _locationSortHelper;
        private IDataShaper<Location> _locationDataShaper;

        private IRoomRepository _room;
        private ISortHelper<Room> _roomSortHelper;
        private IDataShaper<Room> _roomDataShaper;

        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_repoContext, _departmentSortHelper, _departmentDataShaper);
                }

                return _department;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext, _employeeSortHelper, _employeeDataShaper);
                }

                return _employee;
            }
        }

        public ILocationRepository Location
        {
            get
            {
                if (_location == null)
                {
                    _location = new LocationRepository(_repoContext, _locationSortHelper, _locationDataShaper);
                }

                return _location;
            }
        }

        public IRoomRepository Room
        {
            get
            {
                if (_room == null)
                {
                    _room = new RoomRepository(_repoContext, _roomSortHelper, _roomDataShaper);
                }

                return _room;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext, ISortHelper<Department> departmentSortHelper, IDataShaper<Department> departmentDataShaper, ISortHelper<Employee> employeeSortHelper, IDataShaper<Employee> employeeDataShaper, ISortHelper<Location> locationSortHelper, IDataShaper<Location> locationDataShaper, ISortHelper<Room> roomSortHelper, IDataShaper<Room> roomDataShaper)
        {
            _repoContext = repositoryContext;
            _departmentSortHelper = departmentSortHelper;
            _departmentDataShaper = departmentDataShaper;
            _employeeSortHelper = employeeSortHelper;
            _employeeDataShaper = employeeDataShaper;
            _locationSortHelper = locationSortHelper;
            _locationDataShaper = locationDataShaper;
            _roomSortHelper = roomSortHelper;
            _roomDataShaper = roomDataShaper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
