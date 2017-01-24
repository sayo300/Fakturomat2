using System;
using System.Collections.ObjectModel;
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
    /// Interaction logic for newInvoice.xaml
    /// </summary>
    public partial class newInvoice : Window
    {
        private FakturomatDB _db;
        private Klient klient;
        private ObservableCollection<Products> pro;
        private int newIdInvoice;
        private decimal netto;
        private decimal brutto;
        private decimal vat;


        public newInvoice(Klient klient, FakturomatDB _db)
        {

            InitializeComponent();
            this._db = _db;
            this.klient = klient;

            discribeCb.ItemsSource = new ObservableCollection<String> { "Deska", "Opał", "Inne" } ;
            discriptionAddedCb.ItemsSource = new ObservableCollection<String> { "145X22X1200", "100X22X1200", "120X30x1200" };
            chainCb.ItemsSource = new ObservableCollection<String> { "m3", "szt" };
            taxCb.ItemsSource = new ObservableCollection<String> { "23", "18", "6" };

            try
            {
                newIdInvoice = _db.Invoices.Max(e => e.Id) +1;
            }
            catch {

                newIdInvoice = 1;
            }

        }

    

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            if (pro == null) {

                pro = new ObservableCollection<Products>();
            }

            if (discribeCb.SelectedIndex != -1 && discriptionAddedCb.SelectedIndex != -1 && priceCb.Text != "" && countCb.Text != "" && taxCb.SelectedIndex != -1 && chainCb.SelectedIndex != -1) {

                pro.Add(new Products {
                    chain = chainCb.Text,
                    count = Convert.ToDouble(countCb.Text),
                    description = discribeCb.Text,
                    descriptionAdded = discriptionAddedCb.Text,
                    priceNetto = Convert.ToDecimal(priceCb.Text),
                    tax = taxCb.Text,
                    IdInvoice = newIdInvoice
                  
                });

                productsDG.ItemsSource = pro;
                refreshNetto(Convert.ToDecimal(priceCb.Text), Convert.ToDecimal(countCb.Text));
              


            }
        }

        private void refreshNetto(decimal tmp , decimal countTmp) {
            netto += tmp * countTmp;
            vat = netto * (Convert.ToDecimal(taxCb.Text) / 100);
            brutto = netto + vat;

            nettoLab.Content = netto + " zł";
            vatLab.Content = vat + " zł";
            bruttoLab.Content = brutto + " zł";

        }


        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productsDG.Items.Count > 0)
            {
                var fak = new Invoice
                {
                    Id = newIdInvoice,
                    IdCustomer = klient.Id,
                    netto = netto,
                    brutto = brutto,
                    vat = vat,
                    DateInvoices = DateTime.Today,
                    YearInvoices = DateTime.Today.Year,
                     

                };
                try
                {
                    fak.NumberInvoices = _db.Invoices.Max(s => s.NumberInvoices)+1;
                }
                catch {
                    fak.NumberInvoices = 1;
                }
                
                _db.Invoices.Add(fak);
                foreach (Products tmp in pro)
                {
                    _db.Productss.Add(tmp);

                }
                _db.SaveChanges();
                Close();
            }
            else {
                 MessageBox.Show("Dodaj produkt");
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productsDG.SelectedIndex != -1) {
                var tmp = productsDG.SelectedItem as Products;
                refreshNetto(-tmp.priceNetto, Convert.ToDecimal(tmp.count));
                pro.Remove(tmp);
                try
                {
                    _db.Productss.Remove(tmp);
                }
                catch { }
                    productsDG.ItemsSource = pro;

            }
        }


        
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productsDG.SelectedIndex != -1)
            {
                var tmp = productsDG.SelectedItem as Products;
                discribeCb.Text = tmp.description;
                discriptionAddedCb.Text = tmp.descriptionAdded;
                chainCb.Text = tmp.chain;
                countCb.Text = tmp.count.ToString();
                priceCb.Text = tmp.priceNetto.ToString();
                taxCb.Text = tmp.tax;
                refreshNetto(-tmp.priceNetto, Convert.ToDecimal(tmp.count));

                pro.Remove(tmp);
                try
                {
                    _db.Productss.Remove(tmp);
                }
                catch { }
                    productsDG.ItemsSource = pro;

            }
        }
    }
}
