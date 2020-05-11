using CoreAngularApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngularApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;
        public CustomerRepository(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public bool SaveCustomerReviews(List<CustomersReviews> reviews)
        {
            try
            {
                _context.CustomerReviews.AddRange(reviews);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<CustomersReviews> GetCustomerReviewsByAnsi(string ansi)
        {
            return _context.CustomerReviews.Where(a => a.Ansi.ToLower() == ansi.ToLower()).OrderByDescending(cr => cr.ReviewDate).ToList();
        }
    }
}
