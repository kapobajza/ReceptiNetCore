using Recepti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Repository.KorisnikRepo
{
    public interface IKorisnikRepo : IRepo<Korisnik>
    {
        IEnumerable<Korisnik> GetAllNoAdmins();
        Korisnik GetByUsername(string username);
    }
}
