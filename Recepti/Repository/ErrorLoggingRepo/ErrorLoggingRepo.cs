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

        public ErrorLogging Get(Expression<Func<ErrorLogging, bool>> expression, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .ErrorLogging
                    .FirstOrDefault(expression ?? (x => true));
            }

            return _context
                .ErrorLogging
                .Include(include)
                .FirstOrDefault(expression ?? (x => true));
        }

        public IEnumerable<ErrorLogging> GetAll(Expression<Func<ErrorLogging, bool>> expression = null, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .ErrorLogging
                    .Where(expression ?? (x => true))
                    .ToList();
            }

            return _context
                .ErrorLogging
                .Include(include)
                .Where(expression ?? (x => true))
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
