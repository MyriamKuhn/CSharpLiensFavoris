using LiensFavoris.Repository.config;
using LiensFavoris.Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace LiensFavoris.Repository.Links
{
    public class LinkRepository : BaseRepository, ILinkRepository
    {
        public LinkRepository(IConfiguration configuration): base(configuration) 
        {
        }
        
        public List<LinkModel> GetAllLinks()
        {
            //Je me connecte à la BDD
            var cnn = OpenConnexion();
            
            //Je crée une requête SQL
            string sql = @"
                SELECT
                    l.idLinks,
                    l.title,
                    l.description,
                    l.link,
                    u.idUser,
                    u.forename,
                    u.surname,
                    u.mail
                FROM
                    Links AS l
                INNER JOIN Users AS u ON l.idAuteur = u.idUser
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<LinkModel>();

            //Récupérer le retour et le transformer en objet
            while (reader.Read())
            {
                UserModel auteur = new UserModel(
                    Convert.ToInt32(reader["idUser"]),
                    reader["forename"].ToString(),
                    reader["surname"].ToString(),
                    reader["mail"].ToString()
                );

                var leLien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["title"].ToString(),
                    reader["description"].ToString(),
                    reader["link"].ToString(), 
                    auteur
                );

                maListe.Add(leLien);
            }


            cnn.Close();
            return maListe;
        }
        public LinkModel GetLink(int id)
        {
            //Je me connecte à la BDD
            var cnn = OpenConnexion();

            //Je crée une requête SQL
            string sql = @"
                SELECT
                    l.idLinks,
                    l.title,
                    l.description,
                    l.link,
                    u.idUser,
                    u.forename,
                    u.surname,
                    u.mail
                FROM
                    Links AS l
                INNER JOIN Users AS u ON l.idAuteur = u.idUser
                WHERE l.idLinks = @idLink
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idLink", id);
            var reader = cmd.ExecuteReader();
            LinkModel monLien = null;

            //Récupérer le retour et le transformer en objet
            if (reader.Read())
            {
                UserModel auteur = new UserModel(
                    Convert.ToInt32(reader["idUser"]),
                    reader["forename"].ToString(),
                    reader["surname"].ToString(),
                    reader["mail"].ToString()
                );

                monLien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["title"].ToString(),
                    reader["description"].ToString(),
                    reader["link"].ToString(),
                    auteur
                );
            }

            cnn.Close();
            return monLien;
        }

        public bool EditLink(LinkModel link)
        {
            try
            {
                //Je me connecte à la BDD
                var cnn = OpenConnexion();

                //Je crée une requête SQL
                string sql = @"
                    UPDATE Links
                    SET
                        title = @title,
                        description = @description,
                        link = @link,
                        idAuteur = @idUser
                    WHERE
                        idLinks = @idLink
                    ";

                //Exécuter la requête SQL, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@title", link.Title);
                cmd.Parameters.AddWithValue("@description", link.Description);
                cmd.Parameters.AddWithValue("@link", link.URL);
                cmd.Parameters.AddWithValue("@idUser", link.Auteur.IdUser);
                cmd.Parameters.AddWithValue("@idLink", link.IdLink);

                var nbRowEdited = cmd.ExecuteNonQuery();
                    
                cnn.Close();
                return true;
            }
            catch(Exception e)
            { 
                return false; 
            }
        }

        public bool DeleteLink(int id)
        {
            try
            {
                //Je me connecte à la BDD
                var cnn = OpenConnexion();

                //Je crée une requête SQL
                string sql = @"
                    DELETE FROM Links
                    WHERE
                        idLinks = @idLink
                    ";

                //Exécuter la requête SQL, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idLink", id);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
