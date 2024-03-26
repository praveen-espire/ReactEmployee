using Microsoft.EntityFrameworkCore;
using Repositories.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICommonRepository
    {
        void SetDbContext(TestDbContext context);

        Task<IList<T>> GetAllAsync<T>() where T : class;

        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task<T> GetByIdLongAsync<T>(long id) where T : class;

        Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties) where T : class;

        Task<IList<T>> GetOrderedFilteredList<T, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderby,
                    bool descending, int recordCount = 0, params Expression<Func<T, object>>[] navigationProperties) where T : class;

        IList<T> GetFilteredList<T>(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties) where T : class;

        Task<bool> InsertOrUpdate<T>(T currentItem, Expression<Func<T, bool>> whereClause, params string[] columnToSkipForUpdate)
            where T : class;

        Task<bool> InsertAsync<T>(IEnumerable<T> insertDataList) where T : class;

        Task<bool> UpdateAsync<T>(IEnumerable<T> updateDataList) where T : class;

        Task<bool> InsertEntityAsync<T>(T insertData) where T : class;

        Task<bool> UpdateEntityAsync<T>(T updateData) where T : class;

        Task<T> FindAsync<T>(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] navigationProperties) where T : class;

        T FindRecord<T>(Expression<Func<T, bool>> whereClause) where T : class;

        Task<IEnumerable<T>> GetDataFromSP<T>(string spExecCommand, params object[] spParams) where T : class;

        Task ExecuteSP(string spExecCommand, params object[] spParams);

        Task<bool> RemoveEntityAsync<T>(T removeData) where T : class;

        Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, bool>> order,
           params Expression<Func<T, object>>[] navigationProperties) where T : class;
    }
}
