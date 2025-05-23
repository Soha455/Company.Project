﻿using Company.Project.BLL.Interfaces;
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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context) // Ask CLR to Generate object from CompanyDbContext
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }



        //private readonly CompanyDbContext _Context;

        ////Ask CLR to create object from CompanyDbContext
        //public EmployeeRepository(CompanyDbContext context)
        //{
        //    _Context = context;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _Context.Employees.ToList();
        //}
        //public Employee? Get(int id)
        //{
        //    return _Context.Employees.Find(id);
        //}
        //public int Add(Employee employee)
        //{
        //    _Context.Employees.Add(employee);
        //    return _Context.SaveChanges();

        //}
        //public int Update(Employee employee)
        //{
        //    _Context.Employees.Update(employee);
        //    return _Context.SaveChanges();
        //}
        //public int Delete(Employee employee)
        //{
        //    _Context.Employees.Remove(employee);
        //    return _Context.SaveChanges();
        //}
    }
}
