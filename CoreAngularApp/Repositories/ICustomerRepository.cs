using CoreAngularApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngularApp.Repositories
{
    public interface ICustomerRepository
    {
        bool SaveCustomerReviews(List<CustomersReviews> reviews);
        List<CustomersReviews> GetCustomerReviewsByAnsi(string ansi);
    }
}
