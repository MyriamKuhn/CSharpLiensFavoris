using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public class LinkModel
    {
        public int IdLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string URL { get; set; }

        //TODO : auteur => Ajouter en objet... à voir plus tard
    }
}
