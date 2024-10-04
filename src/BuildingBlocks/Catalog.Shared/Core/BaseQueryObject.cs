using System.Linq.Expressions;
using LinqKit;

namespace Catalog.Shared.Core
{
    public interface IBaseQueryObject<TEntity>
    {
        Expression<Func<TEntity, bool>> Expression { get; }
        IBaseQueryObject<TEntity> And(Expression<Func<TEntity, bool>> query);
        IBaseQueryObject<TEntity> Or(Expression<Func<TEntity, bool>> query);
        IBaseQueryObject<TEntity> And(IBaseQueryObject<TEntity> queryObject);
        IBaseQueryObject<TEntity> Or(IBaseQueryObject<TEntity> queryObject);
    }

    public abstract class BaseQueryObject<TEntity> : IBaseQueryObject<TEntity>
    {
        private Expression<Func<TEntity, bool>> _query;
        public virtual Expression<Func<TEntity, bool>> Expression => _query;

        public IBaseQueryObject<TEntity> And(Expression<Func<TEntity, bool>> query)
        {
            _query = _query == null ? query : _query.And(query.Expand());
            return this;
        }

        public IBaseQueryObject<TEntity> Or(Expression<Func<TEntity, bool>> query)
        {
            _query = _query == null ? query : _query.Or(query.Expand());
            return this;
        }

        public IBaseQueryObject<TEntity> And(IBaseQueryObject<TEntity> queryObject)
        {
            return And(queryObject.Expression);
        }

        public IBaseQueryObject<TEntity> Or(IBaseQueryObject<TEntity> queryObject)
        {
            return Or(queryObject.Expression);
        }
    }
}

