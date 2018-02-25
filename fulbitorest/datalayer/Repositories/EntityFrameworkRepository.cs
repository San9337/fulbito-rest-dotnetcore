using datalayer.Contracts;
using datalayer.FulbitoContext;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using model.Exceptions;

namespace datalayer.Repositories
{
    /// <summary>
    /// Non generic implementation to handle scenarios where no binding to a specific entity is appropriate
    /// </summary>
    public abstract class EntityFrameworkRepository
    {
        protected readonly FulbitoDbContext FulbitoContext;

        protected EntityFrameworkRepository(FulbitoDbContext context)
        {
            FulbitoContext = context;
        }
    }

    /// <summary>
    /// Repository attached to a specific entity type 
    /// </summary>
    public abstract class EntityFrameworkRepository<T> : IRepository<T> where T :class, IEntity
    {
        protected readonly FulbitoDbContext FulbitoContext;

        protected EntityFrameworkRepository(FulbitoDbContext context)
        {
            FulbitoContext = context;
        }

        public bool Exists(int id)
        {
            return FulbitoContext.Set<T>().Any(e => e.Id == id);
        }

        public virtual T Get(int id)
        {
            T entity = FulbitoContext.Set<T>().FirstOrDefault(x => x.Id == id);
            if (entity == null)
                throw new FulbitoException(nameof(T) + " with id "+id+" doesnt exist");
            return entity;
        }


        public void Save(T entityWithChanges)
        {
            FulbitoContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            FulbitoContext.Set<T>().Remove(entity);

            FulbitoContext.SaveChanges();
        }

        public virtual void Add(T newEntity)
        {
            FulbitoContext.Set<T>().Add(newEntity);

            FulbitoContext.SaveChanges();
        }

        public virtual IEnumerable<T> All()
        {
            return FulbitoContext.Set<T>();
        }

        
    }
}
