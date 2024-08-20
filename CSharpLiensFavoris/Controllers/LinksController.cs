using CSharpLiensFavoris.Models;
using LiensFavoris.Repository.Links;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpLiensFavoris.Controllers
{
    //TODO : Récupérer le modèle depuis le repository
    public class LinksController : Controller
    {
        private readonly ILinkRepository _linkRepository;

        public LinksController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public IActionResult Index()
        {
            var vm = new ListLinksViewModel()
            {
                LstLinks = _linkRepository.GetAllLinks()
            };
            return View(vm);
        }
    }
}
