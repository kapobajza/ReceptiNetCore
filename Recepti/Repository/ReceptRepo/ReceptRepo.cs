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

        public Recept Get(int id)
        {
            return _context
                .Recepti
                .AsNoTracking()
                .FirstOrDefault(x => x.ReceptId == id);
        }

        public IEnumerable<Recept> GetAll()
        {
            return _context
                .Recepti
                .OrderByDescending(x => x.DatumObjave)
                .ToList();
        }

        public IEnumerable<Recept> GetAllFromKorisnik(int id)
        {
            return _context
                .Recepti
                .Include(x => x.Korisnik)
                .Where(x => x.KorisnikId == id)
                .OrderByDescending(x => x.DatumObjave)
                .ToList();
        }

        public IEnumerable<Recept> GetAllPrivateFilter(bool includePrivate = false)
        {
            return _context
                .Recepti
                .Include(x => x.Korisnik)
                .Where(x => includePrivate ? true : !x.Privatan)
                .OrderByDescending(x => x.DatumObjave)
                .ToList();
        }

        public IEnumerable<Recept> GetAllWithFilters(int korisnikId, string keyword = "", bool isHomePage = true, string kategorija = "")
        {
            return _context
                .Recepti
                .Include(x => x.Korisnik)
                .Where(x => (
                    x.Naziv.Contains(keyword) || string.IsNullOrEmpty(keyword))
                    && (!isHomePage ? x.KorisnikId == korisnikId : true)
                    && (string.IsNullOrEmpty(kategorija) ? true : x.Kategorija == kategorija))
                .OrderByDescending(x => x.DatumObjave)
                .ToList();
        }

        public IEnumerable<Recept> GetAllWithKorisnik()
        {
            return _context
                .Recepti
                .Include(x => x.Korisnik)
                .OrderByDescending(x => x.DatumObjave)
                .ToList();
        }

        public Recept GetWithKorisnik(int id)
        {
            return _context
                .Recepti
                .Include(x => x.Korisnik)
                .AsNoTracking()
                .FirstOrDefault(x => x.ReceptId == id);
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
