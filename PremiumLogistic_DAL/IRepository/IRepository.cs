namespace PremiumLogistic_DAL.IRepository;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    Task<TEntity> GetByIdAsync(int id);
    void Insert(TEntity entity);
    void InsertRange(IList<TEntity> entities);
    void Update(TEntity entity);
    void Delete(int id);
    void Delete(TEntity entity);
    void Delete(Expression<Func<TEntity, bool>> where);
    TEntity Get(Expression<Func<TEntity, bool>> where);
    IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
    Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
    Task<List<TEntity>> IncludeAsync(Expression<Func<TEntity, object>> includeExpression);
    Task<PagedResponseOffset<TEntity>> GetWithOffsetPagination(int pageNumber, int pageSize);
}
