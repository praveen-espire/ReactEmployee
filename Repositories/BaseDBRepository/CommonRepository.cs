using Microsoft.EntityFrameworkCore;
using Repositories.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private DbContext _dbContext;
        public CommonRepository() { }
        public void SetDbContext(TestDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IList<T>> GetAllAsync<T>() where T : class
        {
            List<T> list = default;
            List<T> taskResult = await Task.Run(() =>
            {
                IQueryable<T> dbQuery = _dbContext.Set<T>();
                list = dbQuery
                .AsNoTracking()
                //.Where(entity => entity.GetType().GetProperty("IsActive").GetValue(entity) as bool? == true)
                .ToList<T>();
                return list;
            });

            return taskResult;
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdLongAsync<T>(long id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, bool>> order,
            params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            List<T> list = default;
            List<T> taskResult = await Task.Run(() =>
            {
                IQueryable<T> dbQuery = _dbContext.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                if (where != null)
                {
                    list = dbQuery
                        .AsNoTracking()
                         .Where(where)
                         .OrderBy(order)
                        .ToList<T>();
                }
                else
                {
                    list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
                }
                return list;
            });

            return taskResult;
        }

        public async Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            List<T> list = default;
            List<T> taskResult = await Task.Run(() =>
            {
                IQueryable<T> dbQuery = _dbContext.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                if (where != null)
                {
                    list = dbQuery
                        .AsNoTracking()
                         .Where(where)
                        .ToList<T>();
                }
                else
                {
                    list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
                }
                return list;
            });

            return taskResult;
        }

        public async Task<IList<T>> GetOrderedFilteredList<T, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderby,
                    bool descending, int recordCount = 0, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            List<T> list = default;
            List<T> taskResult = await Task.Run(() =>
            {
                IQueryable<T> dbQuery = _dbContext.Set<T>();
                if (descending)
                {
                    dbQuery = dbQuery.OrderByDescending(orderby);
                }
                else
                {
                    dbQuery = dbQuery.OrderBy(orderby);
                }

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                if (recordCount > 0)
                {
                    list = dbQuery
                        .AsNoTracking()
                        .Where(where)?.Take(recordCount)?
                        .ToList<T>();
                    return list;
                }

                list = dbQuery
                        .AsNoTracking()
                        .Where(where)?
                        .ToList<T>();
                return list;
            });
            return taskResult;
        }

        public IList<T> GetFilteredList<T>(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            IQueryable<T> dbQuery = _dbContext.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            }

            List<T> list = dbQuery
    .AsNoTracking()
    .Where(where)
    .ToList();
            return list;
        }

        /// <summary>
        /// Insert (if new item) Or Update (if item already exists based on primary-key supplied)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentItem"></param>
        /// <param name="whereClause"></param>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public Task<bool> InsertOrUpdate<T>(T currentItem, Expression<Func<T, bool>> whereClause, params string[] columnToSkipForUpdate)
            where T : class
        {
            IQueryable<T> dbQuery = _dbContext.Set<T>();
            T existingItem = dbQuery.FirstOrDefault(whereClause);
            if (existingItem == null)
            {
                _dbContext.Set<T>().Add(currentItem);
            }
            else
            {
                foreach (string column in columnToSkipForUpdate)
                {
                    System.Reflection.PropertyInfo propInfo = existingItem.GetType().GetProperty(column);
                    object propValue = propInfo.GetValue(existingItem);
                    propInfo.SetValue(currentItem, propValue);
                }
                _dbContext.Entry(existingItem).CurrentValues.SetValues(currentItem);
            }
            _dbContext.SaveChanges();
            return Task.FromResult(true);
        }

        public async Task<bool> InsertAsync<T>(IEnumerable<T> insertDataList) where T : class
        {
            _dbContext.Set<T>().AddRange(insertDataList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync<T>(IEnumerable<T> updateDataList) where T : class
        {
            _dbContext.Set<T>().UpdateRange(updateDataList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertEntityAsync<T>(T insertData) where T : class
        {
            _dbContext.Set<T>().Add(insertData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveEntityAsync<T>(T removeData) where T : class
        {
            _dbContext.Set<T>().Remove(removeData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEntityAsync<T>(T updateData) where T : class
        {
            _dbContext.Set<T>().Update(updateData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            IQueryable<T> dbQuery = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            }

            return await dbQuery.FirstOrDefaultAsync(whereClause);
        }

        public T FindRecord<T>(Expression<Func<T, bool>> whereClause) where T : class
        {
            IQueryable<T> dbQuery = _dbContext.Set<T>();
            return dbQuery.FirstOrDefault(whereClause);
        }

        public async Task<IEnumerable<T>> GetDataFromSP<T>(string spExecCommand, params object[] spParams) where T : class
        {
            List<T> list = default;
            List<T> taskResult = await Task.Run(() =>
            {
                DbSet<T> dbQuery = _dbContext.Set<T>();
                list = dbQuery.FromSqlRaw(spExecCommand, spParams)?.ToList();
                return list;
            });
            return taskResult;
        }

        public async Task ExecuteSP(string spExecCommand, params object[] spParams)
        {
            await _dbContext.Database?.ExecuteSqlRawAsync(spExecCommand, spParams);
        }
    }
}