using LiensFavoris.Repository.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public class LinkModel
    {
        public LinkModel(int idLink, string title, string description, string Url, UserModel auteur) 
        { 
            IdLink = idLink;
            Title = title;
            Description = description;
            URL = Url;
            Auteur = auteur;
        }
        public int IdLink { get; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string URL { get; set; }

        public UserModel Auteur {  get; set; }
    }
}
