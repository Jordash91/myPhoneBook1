using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        
        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "192.168.0.55";
            database = "phonebook";
            uid = "jordan";
            password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                       Console.WriteLine("Cannot connect to server. Contact me");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password - fix your shit");
                        break;
                }
                return false;

            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string name, string tel)
        {
            string query = "INSERT INTO contacts (name, tel) VALUES (@name, @tel)";
            if (this.OpenConnection())
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Prepare();
                //Execute command
                cmd.ExecuteNonQuery();
                //Close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string name, string tel)
        {
            string query = "UPDATE contacts SET tel=@tel WHERE name=@name";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Prepare();

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM contacts WHERE name='Geoff'";

            if (this.CloseConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<Contact> Select(string name)
        {
            string query = "SELECT * FROM contacts WHERE name LIKE @name";
            
            //Create a list to store the result
            var list = new List<Contact>();
            
            //Open connection
            if (this.OpenConnection())
            {
                //Create command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Prepare();

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    var contact = new Contact();
                    //contact.Id = dataReader.GetInt32(0);
                    contact.Name = dataReader.GetString(0);
                    contact.Tel = dataReader.GetString(1);
                    list.Add(contact);
                }
                //close Data Reader
                dataReader.Close();
                this.CloseConnection();
            }
            return list;

        }
    }
    public class Contact
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
