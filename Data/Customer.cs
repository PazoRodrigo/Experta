using System.Collections.Generic;

namespace IngresoSML2.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}