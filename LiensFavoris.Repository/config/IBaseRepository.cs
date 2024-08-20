using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.config
{
    public interface IBaseRepository
    {
        public MySqlConnection OpenConnexion(); 
    }
}
