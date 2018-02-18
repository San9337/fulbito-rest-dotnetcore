﻿using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Contracts
{
    public interface IRepository<T> where T:class
    {
        T Get(int id);
        void Save(T entityWithChanges);
        void Delete(int id);
        void Add(T newEntity);
        IEnumerable<T> All();
    }
}
