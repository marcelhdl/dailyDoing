using System;
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
    /// Interaktionslogik für CreateLending.xaml
    /// </summary>
    public partial class CreateLending : Window
    {
        DBService db = new DBService();
        int cid;
        int userID;
        string title;
        string desc;
        string category;
        string priority;
        string timestamp_lendback;
        MainWindow main;

        public CreateLending(int cid, int userID, string title, string desc, string category, string priority, string timestamp_lendback, MainWindow main)
        {
            this.cid = cid;
            this.userID = userID;
            this.title = title;
            this.desc = desc;
            this.category = category;
            this.priority = priority;
            this.timestamp_lendback = timestamp_lendback;
            InitializeComponent();
            this.main = main;
        }

        //Informationen aus Textboxen nehmen und daraus einen neuen Kontakt erzeugen
        private void btn_createLending_Click(object sender, RoutedEventArgs e)
        {
            string firstname = txt_Firstname.Text;
            string name = txt_Name.Text;
            string email = txt_email.Text;
            db.createLending(db.createconnectionstring(),userID,cid,title,desc,category,priority,timestamp_lendback);
            main.updateAllLendingsBox();
            Close();

        }
    }
}
