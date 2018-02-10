using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Contracts
{
    public interface IRepository<T> where T:class
    {
        void Add(T newEntity);
        IEnumerable<T> All();
    }
}
