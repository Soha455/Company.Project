using Company.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetByNameAsync(string name);

        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);
        //int Add(Employee department);
        //int Update(Employee department);
        //int Delete(Employee department);
    }
}
