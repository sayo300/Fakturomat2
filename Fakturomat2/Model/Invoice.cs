using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturomat2.Model
{
   public class Invoice
    {
        public int Id { get; set; }

        public int IdCustomer { get; set; }
        public int NumberInvoices { get; set; }
        public int YearInvoices { get; set; }
        public DateTime DateInvoices { get; set; }
        public decimal netto { get; set; }
        public decimal brutto { get; set; }
        public decimal vat { get; set; }

        public string getNumberInvoicesAll() {
            return NumberInvoices + "/" + YearInvoices;
        }
    }
}
