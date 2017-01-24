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
using Fakturomat2.DB;
using Fakturomat2.Model;

namespace Fakturomat2.View
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        private FakturomatDB _db;

  

        public NewCustomer(FakturomatDB db)
        {
            _db = db;
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!nameCompantTxb.Equals("")) {

                var kli = new Klient {
                
                    NameCompany = nameCompantTxb.Text,
                    City = cityTxb.Text,
                    Nip = nipTxb.Text,
                    NumberHouse = houseNumberTxb.Text,
                    Phone = phoneTxb.Text,
                    PostCode = postCodeTxb.Text,
                    Street = streetTxb.Text,
                };
                try
                {
                    kli.Id = _db.Clients.Max().Id;
                }
                catch
                {
                    kli.Id = 1;
                }
                _db.Clients.Add(kli);
                _db.SaveChanges();
                Close();
            }



        }
    }
}
