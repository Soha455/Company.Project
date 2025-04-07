using Company.Project.BLL.Interfaces;
using Company.Project.DAL.Data.Contexts;
using Company.Project.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context) : base(context) // Ask CLR to Generate object from CompanyDbContext
        {
            _context = context;
        }

        public List<Department> GetByName(string name)
        {
            return _context.Departments.Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }


        //private readonly CompanyDbContext _Context;

        ////Ask CLR to create object from CompanyDbContext
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _Context = context;
        //}
        //public IEnumerable<Department> GetAll()
        //{
        //    return _Context.Departments.ToList();
        //}
        //public Department? Get(int id)
        //{
        //    return _Context.Departments.Find(id);
        //}
        //public int Add(Department department)
        //{
        //    _Context.Departments.Add(department);
        //    return _Context.SaveChanges();

        //}
        //public int Update(Department department)
        //{
        //    _Context.Departments.Update(department);
        //    return _Context.SaveChanges();
        //}
        //public int Delete(Department department)
        //{
        //    _Context.Departments.Remove(department);
        //    return _Context.SaveChanges();
        //}
    }
}
