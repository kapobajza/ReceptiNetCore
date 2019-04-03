using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Recepti.Context;
using Recepti.Models;

namespace Recepti.Repository.KorisnikRepo
{
    public class KorisnikRepo : IKorisnikRepo
    {
        private readonly EFContext _context;

        public KorisnikRepo(EFContext context)
        {
            _context = context;
        }

        public void Add(Korisnik item)
        {
            _context.Korisnici.Add(item);
        }

        public Korisnik Get(Expression<Func<Korisnik, bool>> expression, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .Korisnici
                    .FirstOrDefault(expression);
            }

            return _context
                .Korisnici
                .Include(include)
                .FirstOrDefault(expression);
        }

        public IEnumerable<Korisnik> GetAll(Expression<Func<Korisnik, bool>> expression = null, string include = "")
        {
            if (string.IsNullOrEmpty(include))
            {
                return _context
                    .Korisnici
                    .Where(expression ?? (x => true))
                    .ToList();
            }

            return _context
                .Korisnici
                .Include(include)
                .Where(expression ?? (x => true))
                .ToList();
        }

        public void Remove(Korisnik item)
        {
            _context.Korisnici.Remove(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Korisnik item)
        {
            _context.Korisnici.Update(item);
        }
    }
}
