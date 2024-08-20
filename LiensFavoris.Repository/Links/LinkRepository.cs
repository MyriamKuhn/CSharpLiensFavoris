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
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
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

            //Exécuter la requête SQL
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<LinkModel>();

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

            //Récupérer le retour et le transformer en objet

            cnn.Close();
            return maListe;
        }
    }
}
