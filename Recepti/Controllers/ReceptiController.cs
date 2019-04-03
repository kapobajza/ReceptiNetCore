using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recepti.Filters;
using Recepti.Helpers;
using Recepti.Models;
using Recepti.Repository.ReceptRepo;
using Recepti.ViewModels.Recepti;

namespace Recepti.Controllers
{
    [Authorize]
    public class ReceptiController : Controller
    {
        private readonly IReceptRepo _receptRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly HtmlSanitizer _htmlSanitizer;

        public ReceptiController(IReceptRepo receptRepo, IHostingEnvironment hostingEnvironment)
        {
            _receptRepo = receptRepo;
            _hostingEnvironment = hostingEnvironment;
            _htmlSanitizer = new HtmlSanitizer();
        }

        [Route("/Recepti/DodajIzmijeni/{id?}")]
        public IActionResult DodajIzmijeni(int? id)
        {
            var model = new DodajIzmijeniReceptViewModel()
            {
                ReceptId = id ?? 0,
                Kategorije = new List<SelectListItem>()
            };

            if (id != null)
            {
                var recept = _receptRepo.Get(x => x.ReceptId == id.Value);
                model.Kategorija = recept.Kategorija;
                model.Naziv = recept.Naziv;
                model.Priprema = recept.Priprema;
                model.Privatan = recept.Privatan;
                model.Sastav = recept.Sastav;
                model.SlikaURL = recept.SlikaURL;
            }

            model.Kategorije = KategorijeRecepta.GetKategorije(model.Kategorija);

            return View(model);
        }

        [HttpPost]
        [ModelStateValidate]
        [ValidateAntiForgeryToken]
        [Route("/Recepti/DodajIzmijeni/{id?}")]
        public IActionResult DodajIzmijeni(DodajIzmijeniReceptViewModel model)
        {
            var slikaUrl = model.SlikaURL;
            var korisnikId = int.Parse(User.FindFirst(x => x.Type == "Id")?.Value);
            Recept recept = null;

            if (model.ReceptId != 0)
            {
                recept = _receptRepo.Get(x => x.ReceptId == model.ReceptId);
            }

            if (recept != null && recept.KorisnikId != korisnikId)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(slikaUrl))
            {
                if (model.Slika == null)
                {
                    ModelState.AddModelError("Slika", "Molimo da odaberete sliku");
                    return View(model);
                }

                slikaUrl = FileHelpers.GetUniqueFileName(model.Slika.FileName);
                var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "slike-recepata");
                var filePath = Path.Combine(uploadFolder, slikaUrl);

                if (recept != null)
                {
                    System.IO.File.Delete(Path.Combine(uploadFolder, recept.SlikaURL));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Slika.CopyTo(stream);
                }
            }

            _receptRepo.Update(new Recept()
            {
                ReceptId = model.ReceptId,
                DatumObjave = recept == null ? DateTime.Now : recept.DatumObjave,
                Kategorija = model.Kategorija,
                KorisnikId = korisnikId,
                Naziv = model.Naziv,
                Priprema = _htmlSanitizer.Sanitize(model.Priprema),
                Privatan = model.Privatan,
                Sastav = model.Sastav,
                SlikaURL = slikaUrl
            });
            _receptRepo.SaveChanges();

            TempData["poruka"] = "Recept uspješno sačuvan.";

            return RedirectToAction("Index", "Home");
        }

        [Route("/Recepti/{id}")]
        [AllowAnonymous]
        public IActionResult Detalji(int id)
        {
            var recept = _receptRepo.Get(x => x.ReceptId == id, "Korisnik");

            if (recept == null)
            {
                return NotFound();
            }

            var model = new DetaljiReceptaViewModel()
            {
                DatumObjave = recept.DatumObjave.ToString("dd.MM.yyyy. HH:mm"),
                Kategorija = recept.Kategorija,
                Korisnik = $"{recept.Korisnik.Ime} {recept.Korisnik.Prezime}",
                Naziv = recept.Naziv,
                Priprema = _htmlSanitizer.Sanitize(recept.Priprema),
                ReceptId = recept.ReceptId,
                Sastav = new List<string>(),
                SlikaURL = recept.SlikaURL
            };

            var sastavParts = recept.Sastav.Split(",");

            foreach (var item in sastavParts)
            {
                model.Sastav.Add(item?.Trim());
            }

            return View(model);
        }

        [Route("/MojiRecepti")]
        public IActionResult MojiRecepti()
        {
            int.TryParse(User.FindFirst(x => x.Type == "Id")?.Value, out int id);
            var recepti = _receptRepo.GetAll(x => x.KorisnikId == id);
            var model = InstantiateKorisnikReceptiVM(id, recepti, false, true);

            return View(model);
        }

        [Route("/Recepti/Search")]
        [AllowAnonymous]
        public IActionResult Search(string keyword = "", bool isHomePage = true, string kategorija = "")
        {
            int.TryParse(User.FindFirst(x => x.Type == "Id")?.Value, out int korisnikId);

            keyword = keyword?.Trim();
            var recepti = GetReceptiWithFilters(korisnikId, keyword, isHomePage, kategorija);
            var model = InstantiateKorisnikReceptiVM(korisnikId, recepti, isHomePage);

            return PartialView("~/Views/Recepti/_ListaRecepata.cshtml", model);
        }

        [Route("/Recepti/FilterPoKategoriji")]
        [AllowAnonymous]
        public IActionResult FilterPoKategoriji(string kategorija, string keyword = "", bool isHomePage = true)
        {
            int.TryParse(User.FindFirst(x => x.Type == "Id")?.Value, out int korisnikId);
            var recepti = GetReceptiWithFilters(korisnikId, keyword, isHomePage, kategorija);
            var model = InstantiateKorisnikReceptiVM(korisnikId, recepti, isHomePage);

            return PartialView("~/Views/Recepti/_ListaRecepata.cshtml", model);
        }

        private IEnumerable<Recept> GetReceptiWithFilters(int korisnikId, string keyword = "", bool isHomePage = true, string kategorija = "")
        {
            var recepti = _receptRepo.GetAll(
                x => (
                    x.Naziv.Contains(keyword) || string.IsNullOrEmpty(keyword))
                    && (!isHomePage ? x.KorisnikId == korisnikId : true)
                    && (string.IsNullOrEmpty(kategorija) ? true : x.Kategorija == kategorija),
                "Korisnik"
            );

            return recepti;
        }

        private KorisnikReceptiViewModel InstantiateKorisnikReceptiVM(int korisnikId, IEnumerable<Recept> recepti, bool isHomePage, bool withKategorije = false)
        {
            var model = new KorisnikReceptiViewModel()
            {
                KorisnikId = korisnikId,
                Recepti = new List<ReceptiViewModel>()
            };

            foreach (var item in recepti)
            {
                model.Recepti.Add(new ReceptiViewModel()
                {
                    ReceptId = item.ReceptId,
                    DatumObjaveFull = item.DatumObjave.ToString("dd.MM.yyyy. HH:mm"),
                    DatumObjave = CustomTimeFunctions.TimeAgo(item.DatumObjave),
                    Kategorija = item.Kategorija,
                    Naziv = item.Naziv,
                    SlikaURL = item.SlikaURL,
                    KorisnikId = item.KorisnikId,
                    Korisnik = !isHomePage ? "" : $"{item.Korisnik.Ime} {item.Korisnik.Prezime}"
                });
            }

            if (withKategorije)
            {
                model.Kategorije = KategorijeRecepta.GetKategorije("", true);
            }

            return model;
        }

        [Route("/Recepti/Brisi/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Brisi(int id)
        {
            var recept = _receptRepo.Get(x => x.ReceptId == id);

            if (recept == null)
            {
                return NotFound();
            }

            _receptRepo.Remove(recept);
            _receptRepo.SaveChanges();
            TempData["poruka"] = "Recept uspješno izbrisan.";

            return RedirectToAction("Index", "Home");
        }
    }
}