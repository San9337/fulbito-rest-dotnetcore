using datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private List<T> list = new List<T>();
        public void Add(T newEntity)
        {
            list.Add(newEntity);
        }

        public IEnumerable<T> All()
        {
            return list;
        }
    }
}
