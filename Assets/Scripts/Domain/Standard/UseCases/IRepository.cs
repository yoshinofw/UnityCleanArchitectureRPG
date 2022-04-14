namespace UCARPG.Domain.Standard.UseCases
{
    public interface IRepository<TEntity>
    {
        TEntity this[string id] { get; }
        void Add(TEntity entity);
        void Remove(string id);
    }
}