using LiensFavoris.Repository.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public class LinkModel
    {
        public LinkModel()
        {

        }
        public LinkModel(int idLink, string title, string description, string Url, UserModel auteur) 
        { 
            IdLink = idLink;
            Title = title;
            Description = description;
            URL = Url;
            Auteur = auteur;
        }
        public int IdLink { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; } 

        [Required]
        public string URL { get; set; }

        public UserModel Auteur {  get; set; }
    }
}
