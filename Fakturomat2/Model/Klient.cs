using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturomat2.Model
{
   public class Klient
    {
      
        public int Id { get; set; }
        public string NameCompany { get; set; }
        public string Nip { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }

        public string getAddress() {
            return City + " ul. " + Street + " nr. domu " + NumberHouse + " kod pocztowy: " + PostCode;
        }

    }
}
