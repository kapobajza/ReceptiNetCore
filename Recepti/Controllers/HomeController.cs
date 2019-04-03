﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recepti.Context;
using Recepti.Helpers;
using Recepti.Models;
using Recepti.Repository.ReceptRepo;
using Recepti.ViewModels;
using Recepti.ViewModels.Recepti;

namespace Recepti.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReceptRepo _receptRepo;
        private readonly EFContext _context;

        public HomeController(IReceptRepo receptRepo, EFContext context)
        {
            _receptRepo = receptRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            var aa = User.IsInRole("Admin");
            var recepti = _receptRepo.GetAll(x => !x.Privatan, "Korisnik");
            int.TryParse(User.FindFirst(x => x.Type == "Id")?.Value, out int id);
            var model = new KorisnikReceptiViewModel()
            {
                Recepti = new List<ReceptiViewModel>(),
                Kategorije = KategorijeRecepta.GetKategorije("", true),
                KorisnikId = id
            };

            foreach (var item in recepti)
            {
                model.Recepti.Add(new ReceptiViewModel()
                {
                    ReceptId = item.ReceptId,
                    DatumObjaveFull = item.DatumObjave.ToString("dd.MM.yyyy. HH:mm"),
                    DatumObjave = CustomTimeFunctions.TimeAgo(item.DatumObjave),
                    Kategorija = item.Kategorija,
                    Korisnik = $"{item.Korisnik.Ime} {item.Korisnik.Prezime}",
                    Naziv = item.Naziv,
                    SlikaURL = item.SlikaURL,
                    KorisnikId = item.KorisnikId
                });
            }

            return View(model);
        }

        public IActionResult Attack(string ime)
        {
            var query = "SELECT * FROM Korisnici WHERE Ime='" + ime + "'";
            var searchedUsers = _context.Set<Korisnik>().FromSql(query).ToList();
            return View("Attack", searchedUsers);
        }

        public string GetCookie(string cookie)
        {
            return cookie;
        }
    }
}