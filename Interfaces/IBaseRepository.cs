﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetById(long id);
        T Create();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(long id);
        void Refresh(T entity);
    }
}
