using LiensFavoris.Repository.config;
using LiensFavoris.Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;

namespace LiensFavoris.Repository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<UserModel> GetAllUsers()
        {
            //Je me connecte à la BDD
            var cnn = OpenConnexion();

            //Je crée une requête SQL
            string sql = @"
                SELECT
                    u.idUser,
                    u.forename,
                    u.surname,
                    u.mail
                FROM
                    Users AS u
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<UserModel>();

            //Récupérer le retour et le transformer en objet
            while (reader.Read())
            {
                UserModel auteur = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    UserForeName = reader["forename"].ToString(),
                    UserSurName = reader["surname"].ToString(),
                    UserEmail = reader["mail"].ToString()
                };

                maListe.Add(auteur);
            }


            cnn.Close();
            return maListe;
        }

        public UserModel GetById(int id)
        {
            //Je me connecte à la BDD
            var cnn = OpenConnexion();

            //Je crée une requête SQL
            string sql = @"
                SELECT
                    u.idUser,
                    u.forename,
                    u.surname,
                    u.mail
                FROM
                    Users AS u
                WHERE
                    u.idUser = @id
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            UserModel monUser = null;

            //Récupérer le retour et le transformer en objet
            if (reader.Read())
            {
                monUser = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    UserForeName = reader["forename"].ToString(),
                    UserSurName = reader["surname"].ToString(),
                    UserEmail = reader["mail"].ToString()
                };
            }


            cnn.Close();
            return monUser;
        }

        public void CreateUser(UserModel user)
        {
            //Je me connecte à la BDD
            var cnn = OpenConnexion();

            //Je crée une requête SQL
            string sql = @"
                INSERT INTO Users (forename, surname, mail)
                VALUES
                (@forename, @surname, @mail)
                ";

            //Exécuter la requête SQL, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@forename", user.UserForeName);
            cmd.Parameters.AddWithValue("@surname", user.UserSurName);
            cmd.Parameters.AddWithValue("@mail", user.UserEmail);

            var reader = cmd.ExecuteNonQuery();
            
            cnn.Close();
        }
    }
}
