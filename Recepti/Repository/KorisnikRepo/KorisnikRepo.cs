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

        public Korisnik Get(int id)
        {
            return _context
                .Korisnici
                .FirstOrDefault(x => x.KorisnikId == id);
        }

        public IEnumerable<Korisnik> GetAll()
        {
            return _context
                .Korisnici
                .ToList();
        }

        public IEnumerable<Korisnik> GetAllNoAdmins()
        {
            return _context
                .Korisnici
                .Where(x => x.Uloga != "Admin")
                .ToList();
        }

        public Korisnik GetByUsername(string username)
        {
            return _context
                .Korisnici
                .FirstOrDefault(x => x.KorisnickoIme == username);
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
