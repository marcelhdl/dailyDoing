using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using System.Data;
//using System.Windows;
//using System.Windows.Input;
using System.IO;

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

        public DBService()
        {
            StreamReader reader = new StreamReader("C:" +Environment.ExpandEnvironmentVariables("%HOMEPATH%") + "\\.connectionDB_DailyDoing.cfg");
            string connect =reader.ReadLine();
            string[] allDataForDB = connect.Split(';');
            this.DBConnection = allDataForDB[2];
            this.DBName = allDataForDB[3];
            this.DBPass = allDataForDB[1];
            this.DBUser = allDataForDB[0];
            this.DBPort = Convert.ToInt32(allDataForDB[4]);
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

        #region methods for contacts/user requests

        //Request all contacts for an specific user.
        public List<string[]> getContacts(MySqlConnection con, int uid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_contacts 
                                    WHERE uid='" + uid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
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
            con.Close();
            return contacts;
        }

        //Request the userID of a specific user.
        public int getUserID(MySqlConnection con, string username)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT uid 
                                    FROM tbl_user 
                                    WHERE username='" + username + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString();
                }
                con.Close();
                return Convert.ToInt32(row);
            }
            con.Close();
            return -1;
        }

        //Request the details of a specific contact.
        public string[] getDetailsFromContacts(MySqlConnection con, int cid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_contacts 
                                    WHERE cid='" + cid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
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
                con.Close();
                return contactinfo;
            }
            con.Close();
            return contactinfo;
        }

        //Creates a contact for a specific user.
        public bool createContact(MySqlConnection con, int uid, string name, string vorname, string mail)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"INSERT INTO
                                    tbl_contacts (uid, name,vorname,mail)
                                    VALUES ('" + uid + "','" + name + "','" + vorname + "','" + mail + "')";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        //Updates an existing contact.
        public bool updateContact(MySqlConnection con, int cid, int uid, string name, string vorname, string mail)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"UPDATE tbl_contacts 
                                    SET uid='" + uid + "', name='" + name + "', vorname='" + vorname + "', mail='" + mail +
                                    "' WHERE cid='" + cid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        //Deletes a specific contact.
        public bool deleteContact(MySqlConnection con, int cid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"DELETE FROM 
                                        tbl_contacts 
                                        WHERE cid='" + cid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        #endregion

        #region methods for lending requests

        public List<string[]> getallLendings(MySqlConnection con, int uid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_lendings_new 
                                    WHERE uid='" + uid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();
            List<string[]> lendings = new List<string[]>();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                String[] contactInfo = row.Split(',');
                lendings.Add(contactInfo);
            }
            con.Close();
            return lendings;
        }

        public List<string[]> getrecentLendings(MySqlConnection con, int uid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_lendings_new 
                                    WHERE uid='" + uid + "' AND get_back='false'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();
            List<string[]> lendings = new List<string[]>();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                String[] contactInfo = row.Split(',');
                lendings.Add(contactInfo);
            }
            con.Close();
            return lendings;
        }
        public List<string[]> getoldLendings(MySqlConnection con, int uid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_lendings_new 
                                    WHERE uid='" + uid + "' AND get_back='true'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();
            List<string[]> lendings = new List<string[]>();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                String[] contactInfo = row.Split(',');
                lendings.Add(contactInfo);
            }
            con.Close();
            return lendings;
        }
        public string[] getDetailsFromLending(MySqlConnection con, int lid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"SELECT * 
                                    FROM tbl_lendings_new 
                                    WHERE lid='" + lid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();
            string[] lendinginfo = new string[6];

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                }
                lendinginfo = row.Split(',');
                con.Close();
                return lendinginfo;
            }
            con.Close();
            return lendinginfo;
        }

        // Create a Lending in the Database Table
        // Please give DateTime.Date
        public bool createLending(MySqlConnection con, int uid, int cid, string title, string desc, string category, string priority, string date_lendback)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"INSERT INTO
                                    tbl_lendings_new
                                    (uid, cid, desc, timestamp_lendback) 
                                    VALUES
                                    ('" + uid + "','" + cid + "','" + desc + "','" + date_lendback + "')";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        //Update a specific landing.
        public bool updateLending(MySqlConnection con, int lid, int cid, string title, string desc, string category, string priority, string date_lendback,string getback)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"UPDATE tbl_lendings_new 
                                    SET cid='" + cid + "'," +
                                    " title='"+ title + "'," +
                                    " desc='" + desc + "'," +
                                    " category='"+ category + "'," +
                                    " priority='" + priority + "'," +
                                    " timestamp_lendback='" + date_lendback +
                                    "' WHERE lid='" + lid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        //Delete a specific landing.
        public bool deleteLending(MySqlConnection con, int lid)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"DELETE FROM 
                                        tbl_lendings_new 
                                        WHERE lid='" + lid + "'";
            MySqlDataReader Reader;
            if (con.State.ToString() == "Open") { }
            else { con.Open(); }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row = Reader.GetValue(i).ToString() + ",";
                }
            }
            con.Close();
            return true;
        }

        #endregion
    }
}
