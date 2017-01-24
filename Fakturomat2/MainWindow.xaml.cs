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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fakturomat2.DB;
using Fakturomat2.View;
using Fakturomat2.Model;

namespace Fakturomat2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            customerDG.ItemsSource = (from b in _db.Clients orderby b.Id select b ).ToList();
            
        }

  
        private void newCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            new NewCustomer(_db).ShowDialog();

            customerDG.ItemsSource = (from b in _db.Clients orderby b.Id select b).ToList();

        }

        private void customerDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (customerDG.SelectedIndex != -1)
            {
                var tmp = customerDG.SelectedItem as Klient;
                nameCustomerLab.Content = tmp.NameCompany;
                nipCustomerLab.Content = tmp.Nip;
                addressCustomerLab.Content = tmp.getAddress();
                try
                {
                    invoiceDG.ItemsSource = (from b in _db.Invoices where b.IdCustomer == tmp.Id orderby b.Id select b).ToList(); 
                }
                catch { }

            }
            else {

                nameCustomerLab.Content = "";
                nipCustomerLab.Content = "";
                addressCustomerLab.Content = "";
            }

        }

        private void newInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customerDG.SelectedIndex != -1) {
                var tmp = customerDG.SelectedItem as Klient;

                new newInvoice(tmp, _db).ShowDialog();
                
            }
            if (customerDG.SelectedIndex != -1) {
                var tmp = customerDG.SelectedItem as Klient;
                invoiceDG.ItemsSource = (from b in _db.Invoices where b.IdCustomer == tmp.Id orderby b.NumberInvoices select b).ToList(); 

            }


        }

        private void invoiceDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (invoiceDG.SelectedIndex != -1) {
                var tmp = invoiceDG.SelectedItem as Invoice;
                nettoLab.Content = tmp.netto;
                bruttoLab.Content = tmp.brutto;
                vatLab.Content = tmp.vat;
                DateInvoiceLab.Content = tmp.DateInvoices.ToShortDateString();
                NumberInvoiceLab.Content = tmp.getNumberInvoicesAll();
                DescriptonOfProductsTxb.Text = getDescriptionProductsForInvoice(tmp.Id);
                


            }
        }



        private string getDescriptionProductsForInvoice(int id) {
            Console.WriteLine(id);
            var query = (from b in _db.Productss where b.IdInvoice == id select b).ToList();

            string pom = "";
            foreach (var t in query) {
                pom = pom + t.ToString() + " ; ";
            }
            return pom;
        }

        private void deleteInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (invoiceDG.SelectedIndex != -1) {
                var tmp = invoiceDG.SelectedItem as Invoice;
                _db.Invoices.Remove(tmp);

                var listProducts = (from b in _db.Productss where b.IdInvoice == tmp.Id select b).ToList();

                foreach (var t in listProducts) {
                    _db.Productss.Remove(t);
                }

                var tmp2 = customerDG.SelectedItem as Klient;
                invoiceDG.ItemsSource = (from b in _db.Invoices where b.IdCustomer == tmp2.Id orderby b.NumberInvoices select b).ToList();
            }
        }

        private void deleteCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customerDG.SelectedIndex != -1) {
                var tmp = customerDG.SelectedItem as Klient;


            }
        }
    }
}
