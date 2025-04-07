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
        IEnumerable<T> GetAll();
        T? Get(int id);
        void Add(T department);
        void Update(T department);
        void Delete(T department);
    }
}
