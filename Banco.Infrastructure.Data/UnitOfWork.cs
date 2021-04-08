using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BancoContext _context;
        public UnitOfWork(BancoContext context)=> _context = context;

        public void Commit()
        {
            _context.SaveChanges();
        }

        
    }
}
