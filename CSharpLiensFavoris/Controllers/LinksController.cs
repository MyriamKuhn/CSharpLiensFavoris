using CSharpLiensFavoris.Models;
using LiensFavoris.Repository.Links;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpLiensFavoris.Controllers
{
    //TODO : Récupérer le modèle depuis le repository
    public class LinksController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ListLinksViewModel()
            {
                LstLinks = new List<LinkModel>()
                {
                    new LinkModel()
                    {
                        IdLink = 1,
                        Title = "test",
                        URL = "https://myriamkuhn.github.io/StudiVoyages/",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum accumsan eleifend felis, sed condimentum velit sollicitudin at. Vivamus vitae erat auctor, auctor lorem vitae, maximus lorem."
                    },
                    new LinkModel()
                    {
                        IdLink = 2,
                        Title = "test2",
                        URL = "https://google.com",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum accumsan eleifend felis, sed condimentum velit sollicitudin at. Vivamus vitae erat auctor, auctor lorem vitae, maximus lorem."

                    },
                    new LinkModel()
                    {
                        IdLink = 3,
                        Title = "test3",
                        URL = "https://google.com",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum accumsan eleifend felis, sed condimentum velit sollicitudin at. Vivamus vitae erat auctor, auctor lorem vitae, maximus lorem."

                    },
                }
            };
            return View(vm);
        }
    }
}
