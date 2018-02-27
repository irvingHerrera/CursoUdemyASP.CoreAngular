using CursoUdemy.Persistence.Contracts;
using System.Threading.Tasks;

namespace CursoUdemy.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CompleteAsync()
        {
            await appDbContext.SaveChangesAsync();
        }
    }
}
