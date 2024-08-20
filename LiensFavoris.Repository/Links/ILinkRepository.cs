using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public interface ILinkRepository
    {
        public List<LinkModel> GetAllLinks();
    }
}
