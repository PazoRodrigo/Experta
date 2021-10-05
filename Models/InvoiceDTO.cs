using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresoSML2.Models
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual ICollection<InvoiceItemDTO> Items { get; set; }
    }
}
