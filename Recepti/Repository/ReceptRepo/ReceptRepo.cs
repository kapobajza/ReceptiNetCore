using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Recepti.Context;
using Recepti.Models;

namespace Recepti.Repository.ReceptRepo
{
    public class ReceptRepo : IReceptRepo
    {
        private readonly EFContext _context;

        public ReceptRepo(EFContext context)
        {
            _context = context;
        }

        public void Add(Recept item)
        {
            _context.Recepti.Add(item);
        }

        public Recept Get(Expression<Func<Recept, bool>> expression, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .Recepti
                    .AsNoTracking()
                    .FirstOrDefault(expression);
            }

            return _context
                .Recepti
                .Include(include)
                .AsNoTracking()
                .FirstOrDefault(expression);
        }

        public IEnumerable<Recept> GetAll(Expression<Func<Recept, bool>> expression = null, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .Recepti
                    .Where(expression ?? (x => true))
                    .ToList();
            }

            return _context
                .Recepti
                .Include(include)
                .Where(expression ?? (x => true))
                .ToList();
        }

        public void Remove(Recept item)
        {
            _context.Recepti.Remove(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Recept item)
        {
            _context.Recepti.Update(item);
        }
    }
}
