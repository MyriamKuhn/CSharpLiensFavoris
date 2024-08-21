using LiensFavoris.Repository.Links;
using LiensFavoris.Repository.User;
using System.Collections.Generic;

namespace CSharpLiensFavoris.Models
{
    public class EditLinkViewModel
    {
        public LinkModel monLien { get; set; }
        public List<UserModel> lstUsers { get; set; }
    }
}
