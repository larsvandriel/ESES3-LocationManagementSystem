using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities;
using LocationManagementSystem.Entities.Extensions;
using LocationManagementSystem.Entities.Helpers;
using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.Parameters;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Repository
{
    public class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly ISortHelper<Employee> _sortHelper;

        private readonly IDataShaper<Employee> _dataShaper;

        public EmployeeRepository(RepositoryContext repositoryContext, ISortHelper<Employee> sortHelper, IDataShaper<Employee> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public PagedList<ShapedEntity> GetAllEmployees(EmployeeParameters employeeParameters)
        {
            var employees = FindAll();

            SearchByName(ref employees, employeeParameters.Name);

            var sortedEmployees = _sortHelper.ApplySort(employees, employeeParameters.OrderBy);
            var shapedEmployees = _dataShaper.ShapeData(sortedEmployees, employeeParameters.Fields).AsQueryable();

            return PagedList<ShapedEntity>.ToPagedList(shapedEmployees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public ShapedEntity GetEmployeeById(Guid employeeId, string fields)
        {
            var employee = FindByCondition(employee => employee.Id.Equals(employeeId)).FirstOrDefault();

            if (employee == null)
            {
                employee = new Employee();
            }

            return _dataShaper.ShapeData(employee, fields);
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            return FindByCondition(i => i.Id.Equals(employeeId)).FirstOrDefault();
        }

        public void UpdateEmployee(Employee dbEmployee, Employee employee)
        {
            dbEmployee.Map(employee);
            Update(dbEmployee);
        }

        private void SearchByName(ref IQueryable<Employee> employees, string employeeName)
        {
            if (!employees.Any() || string.IsNullOrWhiteSpace(employeeName))
            {
                return;
            }

            employees = employees.Where(i => i.Name.ToLower().Contains(employeeName.Trim().ToLower()));
        }
    }
}
