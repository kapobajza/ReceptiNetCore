using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Recepti.Context;
using Recepti.Models;

namespace Recepti.Repository.AuditRepo
{
    public class AuditRepo : IAuditRepo
    {
        private readonly EFContext _context;

        public AuditRepo(EFContext context)
        {
            _context = context;
        }

        public void Add(Audit item)
        {
            _context.Audit.Add(item);
        }

        public Audit Get(Expression<Func<Audit, bool>> expression, string include = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> GetAll(Expression<Func<Audit, bool>> expression = null, string include = "")
        {
            throw new NotImplementedException();
        }

        public void Remove(Audit item)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Audit item)
        {
            throw new NotImplementedException();
        }
    }
}
