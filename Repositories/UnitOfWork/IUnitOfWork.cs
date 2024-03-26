using Microsoft.EntityFrameworkCore.Storage;
using Repositories;
using Repositories.DBEntities;
using System.Threading.Tasks;

namespace CVX.DLANGL.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        IDbContextTransaction StartTransaction(TestDbContext context);

        TestDbContext GetDBContext();

        Task RollbackAsync();

        ICommonRepository _commonRepository { get; }
    }
}
