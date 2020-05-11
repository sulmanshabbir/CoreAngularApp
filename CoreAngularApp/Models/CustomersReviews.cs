using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngularApp.Models
{
    public class CustomersReviews
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string Ansi { get; set; }
    }
}
