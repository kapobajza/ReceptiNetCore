using Microsoft.EntityFrameworkCore;
using Recepti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recepti.Repository.ReceptRepo
{
    public interface IReceptRepo : IRepo<Recept>
    {
        Recept GetWithKorisnik(int id);
        IEnumerable<Recept> GetAllWithKorisnik();
        IEnumerable<Recept> GetAllWithFilters(int korisnikId, string keyword = "", bool isHomePage = true, string kategorija = "");
        IEnumerable<Recept> GetAllPrivateFilter(bool includePrivate = false);
        IEnumerable<Recept> GetAllFromKorisnik(int id);
    }
}
