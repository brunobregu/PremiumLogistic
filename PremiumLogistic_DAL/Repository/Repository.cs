namespace PremiumLogistic_DAL.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
   
    private readonly PremiumLogisticDbContext _context;
    private readonly DbSet<TEntity> Dbset;
    public Repository(PremiumLogisticDbContext context)
    {
        _context = context;
        Dbset = _context.Set<TEntity>();
        
    }

    public IEnumerable<TEntity> GetAll()
    {
        return Dbset.AsNoTracking().AsEnumerable();
    }
    protected IQueryable<TEntity> GetQuery()
    {
        return Dbset.AsNoTracking().AsQueryable();
    }
    public TEntity GetById(int id)
    {
        return Dbset.Find(id);
    }
    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Dbset.AsNoTracking().SingleOrDefaultAsync(BuildLambdaForFindById(id));
    }

    public void Insert(TEntity entity)
    {
        Dbset.Add(entity);
    }
    public virtual void InsertRange(IList<TEntity> entities)
    {
        Dbset.AddRange(entities);
    }
    public void Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity");
        var local = Dbset.Local.ToList();

        foreach (var efloc in local)
        {
            if (GetPropValue(efloc, "Id").Equals(GetPropValue(entity, "Id")))
            {
                _context.Entry(efloc).State = EntityState.Detached;
            }
        }
        Dbset.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

    }
    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null)
            throw new ArgumentNullException("entity");
        Dbset.Remove(entity);
    }
    public void Delete(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity");
        var local = Dbset.Local.ToList();

        foreach (var efloc in local)
        {
            if (GetPropValue(efloc, "Id").Equals(GetPropValue(entity, "Id")))
            {
                _context.Entry(efloc).State = EntityState.Detached;
            }
        }
        Dbset.Attach(entity);
        _context.Entry(entity).State = EntityState.Deleted;
        Dbset.Remove(entity);
    }

    public void Delete(Expression<Func<TEntity, bool>> @where)
    {
        IEnumerable<TEntity> objects = Dbset.Where(where).AsEnumerable();
        foreach (TEntity obj in objects)
            Dbset.Remove(obj);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> @where)
    {
        return Dbset.AsNoTracking().Where(where).FirstOrDefault();
    }

    public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
    {
        return Dbset.AsNoTracking().Where(where).ToList();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Dbset.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where)
    {
        return await Dbset.AsNoTracking().Where(where).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> @where)
    {
        return await Dbset.AsNoTracking().Where(where).ToListAsync();
    }

    protected async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetQuery();
        foreach (var property in includeProperties)
            query = query.Include(property);
        return await query.Where(where).FirstOrDefaultAsync();
    }
    protected TEntity FindOne(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetQuery();
        foreach (var property in includeProperties)
            query = query.Include(property);
        return query.Where(where).FirstOrDefault();
    }
    private Expression<Func<TEntity, bool>> BuildLambdaForFindById(int id)
    {
        var item = Expression.Parameter(typeof(TEntity), "entity");
        var prop = Expression.Property(item, "Id");
        var value = Expression.Constant(id);
        var equal = Expression.Equal(prop, value);
        var lamda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
        return lamda;
    }
    public static object GetPropValue(object src, string propName)
    {
        var val = src.GetType().GetProperty(propName).GetValue(src, null);
        return val;
    }

    public async Task<List<TEntity>> IncludeAsync(Expression<Func<TEntity, object>> includeExpression)
    {
        return await Dbset.Include(includeExpression).ToListAsync();
    }

    public async Task<PagedResponseOffset<TEntity>> GetWithOffsetPagination(int pageNumber, int pageSize)
    {
        var totalRecords = await Dbset.AsNoTracking().CountAsync();
        var entities = await Dbset.AsNoTracking().Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var pagedResponse = new PagedResponseOffset<TEntity>(entities, pageNumber, pageSize, totalRecords);

        return pagedResponse;
    }
}
