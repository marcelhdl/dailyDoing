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
using DailyDoing.classes;

namespace DailyDoing
{
    class DBService
    {
        ServerManager sm = new ServerManager();
        string sql;

        public DBService(){
        }

        #region User
      
        public bool checkPassword(string username, string pw)
        {
            sql = @"SELECT password
                  FROM tbl_user
                  WHERE username ='" + username + "'";
            
            return sm.checkPassword(sql,username,pw);
        }

        public int getUserID(string username)
        {
            sql= @"SELECT uid 
                   FROM tbl_user 
                   WHERE username='" + username + "'";

            return sm.getUserID(sql);
        }
        #endregion

        #region methods for contacts/user requests

        //Request all contacts for an specific user.
        public List<string[]> getContacts(int uid)
        {
            sql= @"SELECT * 
                   FROM tbl_contacts_new 
                   WHERE uid='" + uid + "'";

            return sm.getContacts(sql);
        }

        //Request the details of a specific contact.
        public string[] getDetailsFromContacts(int cid)
        {
            sql = @"SELECT * 
                    FROM tbl_contacts_new 
                    WHERE cid='" + cid + "'";

            return sm.getDetails(sql);
        }

        //Creates a contact for a specific user.
        public bool createContact(Contact contact, int uid)
        {
            sql = @"INSERT INTO
                    tbl_contacts_new (uid, name,firstname,mail,street,housenr,postcode,city,tel,mobile)
                    VALUES ('" 
                    + uid + "','" 
                    + contact.Name + "','" 
                    + contact.Firstname + "','" 
                    + contact.Email + "','" 
                    + contact.Street + "','" 
                    + contact.HouseNumber + "','" 
                    + contact.PostCode + "','" 
                    + contact.City + "','" 
                    + contact.PhoneNumber + "','" 
                    + contact.MobileNumber + "')";

            return sm.Contact(sql);
        }

        //Updates an existing contact.
        public bool updateContact(Contact contact)
        {
            sql = @"UPDATE tbl_contacts_new 
                    SET uid='" + contact.Uid +
                    "', name='" + contact.Name +
                    "', firstname='" + contact.Firstname +
                    "', mail='" + contact.Email +
                    "', street='" + contact.Street +
                    "', housenr='" + contact.HouseNumber +
                    "', postcode='" + contact.PostCode +
                    "', city='" + contact.City +
                    "', tel='" + contact.PhoneNumber +
                    "', mobile='" + contact.MobileNumber +
                    "' WHERE cid='" + contact.Cid + "'";

            return sm.Contact(sql);
        }

        //Deletes a specific contact.
        public bool deleteContact(int cid)
        {
            sql = @"DELETE FROM 
                    tbl_contacts_new 
                    WHERE cid='" + cid + "'";
            return sm.Contact(sql);
        }

        #endregion


        #region methods for lending requests

        public List<string[]> getallLendings(int uid)
        {
            sql = @"SELECT * 
                    FROM tbl_lendings_new 
                    WHERE uid='" + uid + "'";

            return sm.Lendings(sql);
        }

        public List<string[]> getrecentLendings(int uid)
        {
            sql = @"SELECT * 
                    FROM tbl_lendings_new 
                    WHERE uid='" + uid + "' AND get_back='false'";

            return sm.Lendings(sql);
        }
        public List<string[]> getoldLendings(int uid)
        {
            sql = @"SELECT * 
                    FROM tbl_lendings_new 
                    WHERE uid='" + uid + "' AND get_back='true'";
            return sm.Lendings(sql);
        }
        public string[] getDetailsFromLending(int lid)
        {
            sql = @"SELECT * 
                    FROM tbl_lendings_new 
                    WHERE lid='" + lid + "'";
            return sm.getDetails(sql);
        }

        // Create a Lending in the Database Table
        // Please give DateTime.Date
        public bool createLending(int uid, Contact contact ,Lending lending)
        {
           sql = @"INSERT INTO tbl_lendings_new
                   (uid, cid, title, description, category, priority, timestamp_lend, timestamp_lendback, get_back) 
                   VALUES
                   ('" + uid + 
                   "','" + contact.Cid +
                   "','" + lending.Title +
                   "','" + lending.Description +
                   "','" + lending.Category +
                   "','" + lending.Priority + 
                   "','" + lending.Start +
                   "','" + lending.End +
                   "','" + lending.AllreadyBack + "')";

            return sm.Lending(sql);
        }

        //Update a specific landing.
        public bool updateLending(Lending lending)
        {
            sql = @"UPDATE tbl_lendings_new 
                    SET cid='" + lending.Cid + "'," +
                    " title='"+ lending.Title + "'," +
                    " description='" + lending.Description + "'," +
                    " category='"+ lending.Category + "'," +
                    " priority='" + lending.Priority + "'," +
                    " timestamp_lend='" + lending.Start + "'," +
                    " timestamp_lendback='" + lending.End + "'," +
                    " get_back='" + lending.AllreadyBack +
                    "' WHERE lid='" + lending.Lid + "'";
            return sm.Lending(sql);
        }

        //Delete a specific landing.
        public bool deleteLending(int lid)
        {
            sql = @"DELETE FROM 
                    tbl_lendings_new 
                    WHERE lid='" + lid + "'";
            return sm.Lending(sql);
        }

        #endregion
    }
}
