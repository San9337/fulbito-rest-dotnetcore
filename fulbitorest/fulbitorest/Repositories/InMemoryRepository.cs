using datalayer.Contracts;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
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

        public void Delete(int id)
        {
            list.Remove(Get(id));
        }

        public T Get(int id)
        {
            return list.FirstOrDefault(i => i.Id == id);
        }

        public void Save(T entityWithChanges)
        {
            //Do nothing
        }
    }
}
