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
    class ServerManager
    {
        MySqlConnection Con;
        MySqlDataReader Reader;
        MySqlCommand Command;
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

        public bool checkPassword(string sql, string username, string pw)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else { Con.Open(); }
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


        public int getUserID(string sql)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;

            if (Con.State.ToString() == "Open") { }
            else {
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
                        return Convert.ToInt32(row);
                    }
                    Con.Close();
                    return -1;
                }
                catch (MySqlException e)
                {
                    DBException dbex = new DBException(e.Number);
                                       
                }
            }
            return -2;

        }
        #endregion

        #region Contacts
        public List<string[]> getContacts(string sql)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else { Con.Open(); }
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


        public bool Contact(string sql) //Create,Update,Delete ~
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
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


        #endregion

        #region Lending

        public bool Lending(string sql) //Create,Update,Delete
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
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

        public List<string[]> Lendings(string sql)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else { Con.Open(); }
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


        #endregion

        public string[] getDetails(string sql)
        {
            Con = createconnectionstring();
            Command = Con.CreateCommand();
            Command.CommandText = sql;
            if (Con.State.ToString() == "Open") { }
            else { Con.Open(); }
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
            string[] empty = new string[1];
            return empty;
        }
    }

}
