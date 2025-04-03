using Company.Project.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DAL.Data.Configurations
{
    
        public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
        {
            public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
            {
                builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            }
        }
    
}
