using LiensFavoris.Repository.config;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public class LinkRepository : ILinkRepository
    {
        public string ConnectionString { get; set; }
        public LinkRepository(IConfiguration configuration)
        {
            var builder = new MySqlConnectionStringBuilder(
                configuration.GetConnectionString("DefaultConnection"));
            builder.UserID = configuration["DbUserId"];
            builder.Password = configuration["DbPassword"];
            ConnectionString = builder.ConnectionString + ";";
        }
        public List<LinkModel> GetAllLinks()
        {
            //Je me connecte à la BDD
            MySqlConnection cnn = new MySqlConnection(ConnectionString);
            cnn.Open();

            //Je crée une requête SQL
            string sql = @"
                SELECT
                    l.idLinks,
                    l.title,
                    l.description,
                    l.link
                FROM
                    Links AS l
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<LinkModel>();

            //Récupérer le retour et le transformer en objet
            while (reader.Read())
            {
                var leLien = new LinkModel()
                {
                    IdLink = Convert.ToInt32(reader["idLinks"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    URL = reader["link"].ToString()
                };

                maListe.Add(leLien);
            }


            cnn.Close();
            return maListe;
        }
    }
}
