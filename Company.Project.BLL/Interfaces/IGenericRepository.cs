﻿using Company.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.BLL.Interfaces
{
    public interface IGenericRepository<T>  where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T department);
        void Update(T department);
        void Delete(T department);
    }
}
