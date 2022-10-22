using EY.CMS.CORE.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.REPOSITORY.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EyCmsContext _context;
        public UnitOfWork(EyCmsContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
