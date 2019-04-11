using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recepti.Context;
using Recepti.Models;

namespace Recepti.Repository.ErrorLoggingRepo
{
    public class ErrorLoggingRepo : IErrorLoggingRepo
    {
        private readonly EFContext _context;

        public ErrorLoggingRepo(EFContext context)
        {
            _context = context;
        }

        public void Add(ErrorLogging item)
        {
            _context.ErrorLogging.Add(item);
        }

        public ErrorLogging Get(int id)
        {
            return _context
                .ErrorLogging
                .FirstOrDefault(x => x.ErrorLoggingId == id);
        }

        public IEnumerable<ErrorLogging> GetAll()
        {
            return _context
                .ErrorLogging
                .ToList();
        }

        public void Remove(ErrorLogging item)
        {
            _context.ErrorLogging.Remove(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(ErrorLogging item)
        {
            _context.ErrorLogging.Update(item);
        }
    }
}
