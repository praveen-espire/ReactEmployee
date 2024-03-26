using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.DBEntities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CVX.DLANGL.DAL.UnitOfWork
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _dbtransaction;
        private readonly IConfiguration _configuration;
        private TestDbContext _db;

        public UnitOfWork(ICommonRepository commonRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _commonRepository = commonRepository;
        }

        public ICommonRepository _commonRepository { get; private set; }

        public IDbContextTransaction StartTransaction(TestDbContext context)
        {
            _dbtransaction = context.Database.BeginTransaction();
            return _dbtransaction;
        }

        public TestDbContext GetDBContext()
        {
            _db = new TestDbContext(_configuration);
            _commonRepository.SetDbContext(_db);
            return _db;
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
            await _dbtransaction.CommitAsync();
            //Dispose(true);
        }

        public async Task RollbackAsync()
        {
            await _dbtransaction.RollbackAsync();
        }
    }
}
