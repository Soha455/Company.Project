using Company.Project.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Company.Project.DAL.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options): base(options)
        {

        }

        //Apply Congiguration Classes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


        //Set Connection string of DB
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(" Server =. ; Database = CompanyProject ; Trusted_Connection =True ; TrustServerCertificate = True  ");
        //}
        /// <summary>
        /// / in PL main program 
        /// </summary>

        //Mapping Entites to tables in DB 
        public DbSet<Department> Departments { get; set; }
    }
}