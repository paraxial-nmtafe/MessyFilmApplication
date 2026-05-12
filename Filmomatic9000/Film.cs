using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Filmomatic9000
{
    internal class Film : TableConnection
    {
        private string _title;
        private string _description;
        public string title { get; set; }
        public string description { get; set; }

        public void save()
        {
            try
            {
                this.connection.Open();
                string query = "insert into films (title, description) values (@title, @description)";

                MySqlCommand command = new MySqlCommand(query, this.connection);

                command.Parameters.AddWithValue("@title", this.title);
                command.Parameters.AddWithValue("@description", this.description);

                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            } finally { 
                this.connection.Close(); 
            }
           
        }

        public List<string> where(string query)
        {
            List<string> output = new List<string>();
            try
            {
                this.connection.Open();
                string sql_base_query = "select films.id, title, description, GROUP_CONCAT(concat(first_name, ' ', last_name) ORDER BY last_name ASC SEPARATOR ';') AS actor_list " +
                    "from films " +
                    "JOIN film_actor ON films.id = film_actor.film_id " +
                    "JOIN actors ON actors.id = film_actor.actor_id " +
                    $"where description like concat('%', @query, '%') group by films.id;";


                MySqlCommand command = new MySqlCommand(sql_base_query, this.connection);

                command.Parameters.AddWithValue("@query", query);

                command.Prepare();

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    output.Add($"Title: {reader.GetString("title")}, Description: {reader.GetString("description")}, Actors: {reader.GetString("actor_list")}");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.Close();
            }
            return output;
        }
    }
}
