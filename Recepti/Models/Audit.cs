using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Models
{
    [Table("Audit")]
    public class Audit
    {
        public int AuditId { get; set; }
        public string IpAddress { get; set; }
        public string PageAccessed { get; set; }
        public DateTime? LoggedInAt { get; set; }
        public DateTime? LoggedOutAt { get; set; }
        public DateTime AccessDate { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UrlReferrer { get; set; }
        public string Method { get; set; }
        public int ResponseStatusCode { get; set; }
        public bool IsLoggedIn { get; set; }

        public int? KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
