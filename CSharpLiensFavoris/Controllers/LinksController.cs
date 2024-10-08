﻿using CSharpLiensFavoris.Models;
using LiensFavoris.Repository.Links;
using LiensFavoris.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLiensFavoris.Controllers
{
    public class LinksController : Controller
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IUserRepository _userRepository;

        public LinksController(ILinkRepository linkRepository, IUserRepository userRepository)
        {
            _linkRepository = linkRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index(int perPage = 12, int nbPage = 1, string search = "")
        {
            //Je récupère la totalité de mes liens en BDD
            var allLinks = _linkRepository.GetAllLinks();

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                allLinks = allLinks.Where(link =>
                                        link.Title.Contains(search, System.StringComparison.InvariantCultureIgnoreCase) ||
                                        link.Description.Contains(search, System.StringComparison.InvariantCultureIgnoreCase))
                                        .ToList();
            }

            int nbLinkTotal = allLinks.Count();
            //Faire ma pagination
            //LINQ : Take pour prendre un certain nombre d'éléments
            // LINQ : Skip pour passer un certain nombre d'éléments
            allLinks = allLinks.Skip(perPage * (nbPage - 1))
                                 .Take(perPage)
                                 .ToList();

            var vm = new ListLinksViewModel()
            {
                LstLinks = allLinks,
                NbLinksTotalBdd = nbLinkTotal,
                NbPage = nbPage,
                PerPage = perPage,
                Recherche = search
            };

            return View(vm);
        }

        public IActionResult EditLinkPage(int idLink)
        {
            //Je récupère mon Lien
            var leLien = _linkRepository.GetLink(idLink);

            if (leLien == null)
            {
                return NotFound();
            }
            else
            {
                //transformer le modèle de domaine en modèle de vue
                EditLinkViewModel vm = new EditLinkViewModel()
                {
                    monLien = leLien,
                    lstUsers = _userRepository.GetAllUsers()
                };
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult EditLinkAction(EditLinkViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.lstUsers = _userRepository.GetAllUsers();
                return View("EditLinkPage", vm);
            }

            bool isOk = _linkRepository.EditLink(vm.monLien);
            if (isOk)
            {
                TempData["MessageValidation"] = "Le lien a bien été modifié";
                return RedirectToAction("Index");
            }
            else
            {
                vm.lstUsers = _userRepository.GetAllUsers();
                return View("EditLinkPage", vm);
            }
        }

        [HttpPost]
        public IActionResult DeleteLink(EditLinkViewModel vm)
        {
            bool isOk = _linkRepository.DeleteLink(vm.monLien.IdLink);
            if (isOk)
            {
                TempData["MessageValidation"] = "Le lien a bien été supprimé";
                return RedirectToAction("Index");
            }
            else
            {
                vm.lstUsers = _userRepository.GetAllUsers();
                vm.monLien = _linkRepository.GetLink(vm.monLien.IdLink);
                return View("EditLinkPage", vm);
            }
        }
    }
}
