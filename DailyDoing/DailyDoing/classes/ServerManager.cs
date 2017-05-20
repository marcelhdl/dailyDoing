using DailyDoing.classes;
using DailyDoing.classes.ErrorHandlers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing
{
    /// <summary>
    /// Manage the Database-Connections 
    /// </summary>
    class ServerManager
    {
        //Initialise the Objects for the Connection, the DataReader, the Command and the DBError for Errorhandling
        MySqlConnection Con;
        MySqlDataReader Reader;
        MySqlCommand Command;
        DBError dbex;
        string DBUser;
        string DBPass;
        string DBConnection;
        string DBName;
        int DBPort;

        public ServerManager()
        {
            StreamReader reader = new StreamReader("C:" + Environment.ExpandEnvironmentVariables("%HOMEPATH%") + "\\.connectionDB_DailyDoing.cfg");
            string connect = reader.ReadLine();
            string[] allDataForDB = connect.Split(';');
            this.DBConnection = allDataForDB[2];
            this.DBName = allDataForDB[3];
            this.DBPass = allDataForDB[1];
            this.DBUser = allDataForDB[0];
            this.DBPort = Convert.ToInt32(allDataForDB[4]);
        }
        public MySqlConnection createconnectionstring()
        {
            MySqlConnectionStringBuilder con_string = new MySqlConnectionStringBuilder();
            con_string.Server = DBConnection;
            con_string.Port = Convert.ToUInt32(DBPort);
            con_string.UserID = DBUser;
            con_string.Password = DBPass;
            con_string.Database = DBName;
            Con = new MySqlConnection(con_string.ToString());
            return Con;
        }


        #region User

        public int getUserID(string sql) //Returns the UserID
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;

            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();
                    Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            row = Reader.GetValue(i).ToString();
                        }
                        Con.Close();
                        return Convert.ToInt32(row);  //UserID
                    }
                    Con.Close();
                    return -1; //No User Found
                }
                catch (MySqlException e)
                {
                    //dbex.setErrorCode(e.Number);
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }
            }
            return -2; //Exception throwed
        }

        public bool checkPassword(string sql, string username, string pw)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();
                    Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                            row += Reader.GetValue(i).ToString();
                        if (row == pw)
                        {
                            Con.Close();
                            return true;
                        }
                        else
                        {
                            Con.Close();
                            return false;
                        }
                    }
                    Con.Close();
                    return false;
                }
                catch (MySqlException e)
                {
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }
            }
            return false;
        }

        #endregion

        #region Contacts
        public List<string[]> getContacts(string sql) //Returns all Contacts from a User
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();

                    Reader = Command.ExecuteReader();
                    List<string[]> contact = new List<string[]>();

                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            row += Reader.GetValue(i).ToString() + ",";
                        }
                        String[] contactInfo = row.Split(',');
                        contact.Add(contactInfo);
                    }
                    Con.Close();
                    return contact;
                }

                catch (MySqlException e)
                {
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }
            }
            List<string[]> empty = new List<string[]>();
            return empty;
        }

        public bool Contact(string sql) //Create,Update,Delete ~
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            try
            {
                if (Con.State.ToString() == "Open") { }
                else { Con.Open(); }
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        row = Reader.GetValue(i).ToString() + ",";
                    }
                }
                Con.Close();
                return true;
            }
            catch (MySqlException e)
            {
                dbex = new DBError(e.Number);
                dbex.showErrorBox();
            }
            return false;
        }

        #endregion

        #region Lending

        public bool Lending(string sql) //Create,Update,Delete
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();

                    Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            row = Reader.GetValue(i).ToString() + ",";
                        }
                    }
                    Con.Close();
                    return true;
                }
                catch (MySqlException e)
                {
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }

            }
            return false;
        }

        public List<string[]> Lendings(string sql) //Return the Lendings how the sql-string wants
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();

                    Reader = Command.ExecuteReader();
                    List<string[]> lendings = new List<string[]>();

                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            row += Reader.GetValue(i).ToString() + ",";
                        }
                        String[] lendingsinfo = row.Split(',');
                        lendings.Add(lendingsinfo);
                    }
                    Con.Close();
                    return lendings;
                }
                catch (MySqlException e)
                {
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }
            }
            List<string[]> empty = new List<string[]>();
            return empty;
        }

        #endregion

        public string[] getDetails(string sql) //Get the Details of a specific Contact/Lending.
        {
            string[] empty = new string[1];
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else
            {
                try
                {
                    Con.Open();

                    Reader = Command.ExecuteReader();
                    string[] info;
                    while (Reader.Read())
                    {
                        string row = "";
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            row += Reader.GetValue(i).ToString() + ",";
                        }
                        info = row.Split(',');
                        Con.Close();
                        return info;
                    }
                    Con.Close();
                    return empty;
                }

                catch (MySqlException e)
                {
                    dbex = new DBError(e.Number);
                    dbex.showErrorBox();
                }
            }
            return empty;
        }
    }
}


