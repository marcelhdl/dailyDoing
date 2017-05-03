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
    /// Interaktionslogik für UpdateLending.xaml
    /// </summary>
    public partial class UpdateLending : Window
    {
        DBService db = new DBService();
        int lid;
        int cid;
        string title;
        string desc;
        string category;
        string priority;
        string timestamp_lendback;
        bool getback;
        MainWindow main;


        public UpdateLending(int lid, int cid, string title, string desc, string category, string priority, string timestamp_lendback, bool getback, MainWindow main)
        {
            this.lid = lid;
            this.cid = cid;
            this.title = title;
            this.desc = desc;
            this.category = category;
            this.priority = priority;
            this.timestamp_lendback = timestamp_lendback;
            this.getback = getback;
            this.main = main;
            InitializeComponent();

        }
        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateLending(lid,cid,txt_Title_Lending.Text,txt_Desc_lending.Text,txt_Category_lending.Text,txt_Category_lending.Text,txt_lendback_Lending.Text,txt_getback_lending.Text);
            main.updateAllContactsBox();
            Close();
        }
    }
}
