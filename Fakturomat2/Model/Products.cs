using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturomat2.Model
{
   public class Products
    {
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public string description { get; set; }
        public string descriptionAdded { get; set; }
        public double count { get; set; }
        public string chain { get; set; }
        public decimal priceNetto { get; set; }
        public string tax { get; set; }

        public string ToString() {

            return description + " "+ descriptionAdded+" ilo. "+ count+" " + chain ;

        }
    }
}
