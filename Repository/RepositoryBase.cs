using System;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            // 第一种方式 上下文不跟踪检索到的用户，因此当你要将用户保持到数据库时，必须附加它并设置用户状态
            // AsNoTracking可以告诉Entity Framework不要跟踪查询结果。这意味着Entity Framework不对查询返回的实体执行任何其他处理或存储
            // 使用AsNoTracking可以显着提高性能
            return RepositoryContext.Set<T>().AsNoTracking();

            // 第二种情况下，如果使用相同的上下文实例加载和保存用，则不需要这样做，因为跟踪机制会为你处理该情况
            // RepositoryContext.Set<T>()
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

    }
}
