using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresoSML2.Models
{
   
        public class InvoicePostModel
        {

            public DateTime Date { get; set; }

            public int CustomerId { get; set; }

            public List<String> Codes { get; set; }

        }


}
