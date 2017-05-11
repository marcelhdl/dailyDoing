using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing.classes
{
    /// <summary>
    /// Gets, Manipulates and Sets Data from and to DataBase for Lending
    /// </summary>
    class LendingDAO
    {
        DBService db;
        MainWindow main;
        ContactDAO contactService;
        public LendingDAO(MainWindow main, ContactDAO contactService)
        {
            this.main = main;
            db = new DBService();
            this.contactService = contactService;
        }
        public void fillLendingsInListBox()
        {
            List<Lending> lendings = new List<Lending>();
            foreach (string[] lendingInfo in db.getallLendings(main.getCurrentUserID()))
            {
                lendings.Add(new Lending()
                {
                    Lid = Convert.ToInt32(lendingInfo[0]),
                    Uid = Convert.ToInt32(lendingInfo[1]),
                    Cid = Convert.ToInt32(lendingInfo[2]),
                    Title = lendingInfo[3],
                    Description = lendingInfo[4],
                    Category = lendingInfo[5],
                    Priority = lendingInfo[6],
                    Start = Convert.ToDateTime(lendingInfo[7]),
                    End = Convert.ToDateTime(lendingInfo[8]),
                    AllreadyBack = Convert.ToBoolean(lendingInfo[9])
                });
            }
            lendings = lendings.OrderBy(lending => lending.Title).ToList();
            main.lBox_Lendings.ItemsSource = lendings;
        }
        public Lending getSelectedLending() {
            return (Lending)main.lBox_Lendings.SelectedItem;
        }
        public void resetLendingInfo()
        {
            main.DetailViewLendings.DataContext = null;
            main.ContactInLending.DataContext = null;
        }

        internal void setLendingInfo()
        {
            main.DetailViewLendings.DataContext = getSelectedLending();
            main.ContactInLending.DataContext = contactService.getContact(getSelectedLending().Cid);
        }

        internal void createLending(Contact contactForNewLending, Lending lendingToCreate)
        {
            db.createLending(main.getCurrentUserID(), contactForNewLending, lendingToCreate);
        }

        internal void deleteLending()
        {
            db.deleteLending(getSelectedLending().Lid);
        }

        internal void updateLending()
        {
            db.updateLending(getSelectedLending());
        }
    }
}
