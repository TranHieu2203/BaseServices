using Base.DataContractCore.Base;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public static class OrderByMethod
    {
        public static IQueryable<T> OrderByColumn<T>(this IQueryable<T> q, string SortField)
        {
            Boolean isAscending = SortField.Substring(0, 1) == "-";
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, isAscending ? SortField.Substring(1) : SortField);
            var exp = Expression.Lambda(prop, param);

            string method = isAscending ? "OrderByDescending" : "OrderBy";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected BaseServicesContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(
            BaseServicesContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = _context.Set<T>();
        }
        
        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }
    
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    
        public async Task<T?> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }
    
        public async Task<bool> Remove(Guid id)
        {
            var t = await dbSet.FindAsync(id);
    
           if (t != null)
            {
                dbSet.Remove(t);
                return true;
            }
            else
                return false;
        }
    
        public Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }

       
        protected async Task<ResponseBaseList<T>> PageingList<T>(IQueryable<T> queryable, RequestBaseList request)
        {
            try
            {
                if (request.CURRENT_PAGE == 0)
                {
                    request.CURRENT_PAGE = 1;
                }
                if (request.PAGE_SIZE == -1)
                {
                    var data = await queryable.ToListAsync();
                    return new ResponseBaseList<T>
                    {
                        LIST_DATA = data,
                        TOTAL_ROW = data.Count
                    };
                }
                else
                {
                    if (request.PAGE_SIZE == 0)
                    {
                        request.PAGE_SIZE = 20;
                    }
                    int skip = (request.CURRENT_PAGE - 1) * request.PAGE_SIZE;
                    if (request.SORT != null)
                    {
                        queryable = queryable.OrderByColumn(request.SORT);
                    }
                    int count = await queryable.CountAsync();
                    var query = queryable.Skip(skip).Take(request.PAGE_SIZE);
                    var data = await query.ToListAsync();
                    return new ResponseBaseList<T> { LIST_DATA = data, TOTAL_ROW = count };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBaseList<T> { };
            }
        }
    }
}
