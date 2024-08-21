using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.config
{
    //Cette classe va contenir les fonctions de connexion à la BDD
    //On va pouvoir hériter de cette classe
    public class BaseRepository : IBaseRepository
    {
        //J'ai besoin de ma chaine de connexion
        public string ConnectionString { get; set; }
        public BaseRepository(IConfiguration configuration)
        {
            var builder = new MySqlConnectionStringBuilder(
                configuration.GetConnectionString("DefaultConnection"));
                builder.Server = configuration["DbServer"];
                builder.Database = configuration["DbDatabase"];
                builder.UserID = configuration["DbUid"];
                builder.Password = configuration["DbPassword"];
            ConnectionString = builder.ConnectionString + ";";
        }

        public MySqlConnection OpenConnexion()
        {
            try
            {
                MySqlConnection cnn = new MySqlConnection(ConnectionString);
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible de se connecter à la base de données");
            }
        }
    }
}
