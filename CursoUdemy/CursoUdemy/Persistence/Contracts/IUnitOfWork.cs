using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoUdemy.Persistence.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
