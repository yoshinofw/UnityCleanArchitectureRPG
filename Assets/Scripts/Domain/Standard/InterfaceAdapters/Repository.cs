using System.Collections.Generic;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.Standard.Eneities;

namespace UCARPG.Domain.Standard.InterfaceAdapters
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public TEntity this[string id] { get => _entitiesById[id]; }
        private Dictionary<string, TEntity> _entitiesById = new Dictionary<string, TEntity>();

        public void Add(TEntity entity)
        {
            _entitiesById.Add(entity.Id, entity);
        }

        public void Remove(string id)
        {
            if (!_entitiesById.Remove(id))
            {
                throw new KeyNotFoundException();
            }
        }
    }
}