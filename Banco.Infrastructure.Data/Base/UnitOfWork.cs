using Banco.Domain.Contracts;
using Banco.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context) => _context = context;

        private IConsecutivoRepository _consecutivoRepository;
        public IConsecutivoRepository ConsecutivoRepository { get { return _consecutivoRepository ?? (_consecutivoRepository = new ConsecutivoRepository(_context)); } }
        public void Commit()
        {
            _context.SaveChanges();
        }

        
    }
}
