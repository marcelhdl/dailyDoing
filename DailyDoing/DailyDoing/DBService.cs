using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DailyDoing
{
    class DBService
    {
        string DBUser;
        string DBPass;
        string DBConnection;
        string DBName;
        int DBPort;
        MySqlConnection connection;

        #region Constructor and StringBuilder

        public DBService(string DBUser, string DBPass, string DBConnection, string DBName, int DBPort)
        {
            this.DBConnection = DBConnection;
            this.DBName = DBName;
            this.DBPass = DBPass;
            this.DBUser = DBUser;
            this.DBPort = DBPort;
        }

        public MySqlConnection createconnectionstring()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = DBConnection;
            conn_string.Port = Convert.ToUInt32(DBPort);
            conn_string.UserID = DBUser;
            conn_string.Password = DBPass;
            conn_string.Database = DBName;
            connection = new MySqlConnection(conn_string.ToString());
            return connection;
        }

        #endregion

    #region checkLogin

        public bool checkLogin(MySqlConnection con, string username, string pw)
        {
            bool success;
            success = checkUsername(con, username);
            success = checkPassword(con, username, pw);
            return success;
        }
        private bool checkUsername(MySqlConnection con, string username)
        {
            MySqlCommand command = con.CreateCommand(); // Create Object for SQL-Statement
            command.CommandText = "SELECT username FROM tbl_user where username='" + username + "'"; //Set SQL-Statement
            MySqlDataReader Reader; //Create Object Reader
            con.Open(); //Open Connection to Database
            Reader = command.ExecuteReader(); //Execute Command with Reader to get Return-Statement
            while (Reader.Read()) //Read all Rows
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString(); //Save to String
                if (row == username)
                {
                    con.Close(); //Close Database Connection
                    return true; //and return bool
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return false;
        }
        private bool checkPassword(MySqlConnection con, string username, string pw)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT password FROM tbl_user where username ='" + username + "'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString();
                if (row == pw)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return false;
        }
    #endregion

    #region methods for database requests
        public List<string[]> getContacts(MySqlConnection con, int uid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM tbl_contacts WHERE uid='"+uid+"'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();
            List<string[]> contacts = new List<string[]>();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                String[] contactInfo = row.Split(',');
                contacts.Add(contactInfo);
            }
            return contacts;
        }

        public int getUserID(MySqlConnection con, string username)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT uid FROM tbl_user WHERE username='" + username + "'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString();
                }
                return Convert.ToInt32(row);
            }
            return -1;
        }
        public string[] getDetailsFromContacts(MySqlConnection con, int cid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM tbl_contacts WHERE cid='" + cid + "'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();
            string[] contactinfo = new string[6];

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                contactinfo = row.Split(',');
                return contactinfo;
            }
            return contactinfo;
        }

        public bool createContact(MySqlConnection con, int uid, string name, string vorname, string mail)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO tbl_contacts (uid, name,vorname,mail) VALUES ('" + uid + "','" + name + "','" + vorname + "','" + mail + "')";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            return true;
        }
        public bool updateContact(MySqlConnection con, int cid, int uid, string name, string vorname, string mail)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE tbl_contacts SET uid='" + uid + "', name='" + name + "', vorname='" + vorname + "', mail='" + mail + "' WHERE cid='" + cid +"'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            return true;
        }
        public bool deleteContact(MySqlConnection con, int cid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "DELETE FROM tbl_contacts WHERE cid='" + cid + "'";
            MySqlDataReader Reader;
            con.Close();
            con.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            return true;
        }


        #endregion
    }
}
