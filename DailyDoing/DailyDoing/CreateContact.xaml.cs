﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyDoing
{
    /// <summary>
    /// Interaktionslogik für CreateContact.xaml
    /// </summary>
    public partial class CreateContact : Window
    {
        DBService db = new DBService();
        int userID;
        MainWindow main;
        public CreateContact(int userID, MainWindow main)
        {
            InitializeComponent();
            this.userID = userID;
            this.main = main;
        }
        //Informationen aus Textboxen nehmen und daraus einen neuen Kontakt erzeugen
        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            string firstname = txt_Firstname.Text;
            string name = txt_Name.Text;
            string email = txt_email.Text;
            string street = txt_street.Text;
            int houseno = Convert.ToInt32(txt_House_No.Text);
            int postCode = Convert.ToInt32(txt_postcode.Text);
            string city = txt_city.Text;
            string tel = txt_phonenumber.Text;
            string mobile = txt_mobilePhone.Text;
            db.createContact(db.createconnectionstring(),userID, name, firstname, email, street,houseno,postCode,city,tel,mobile);
            main.updateAllContactsBox();
            Close();
            
        }
    }
}
