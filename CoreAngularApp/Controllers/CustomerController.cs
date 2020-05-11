using System;
using System.Collections.Generic;
using System.Linq;
using CoreAngularApp.Repositories;
using CoreAngularApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;

namespace CoreAngularApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustRepo;

        public CustomerController(ICustomerRepository cRepo)
        {
            _CustRepo = cRepo;
        }

        [Route("{ansi}")]
        [HttpGet]
        public ActionResult<List<CustomersReviews>> GetAllCustomers(string ansi)
        {
            var lvReviews = new List<CustomersReviews>();
            if (!string.IsNullOrEmpty(ansi))
            {
                var dbReviews = _CustRepo.GetCustomerReviewsByAnsi(ansi);
                if (dbReviews.Count > 0)
                    return Ok(dbReviews);

                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = web.Load("https://www.amazon.com/product-reviews/" + ansi);
                var nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("review-views"));
                //This loop will run only once
                foreach (HtmlNode node in nodes)
                {
                    var reviewsList = node.Descendants(0).Where(n => n.HasClass("review"));
                    //This loop will run till the numbers of Reviews
                    foreach (HtmlNode item in reviewsList)
                    {
                        var review = new CustomersReviews();
                        review.Ansi = ansi;
                        review.Name = item.Descendants(0).Where(n => n.HasClass("a-profile-name")).FirstOrDefault().InnerHtml;
                        //Review Date
                        var strDate = item.Descendants(0).Where(n => n.HasClass("review-date")).FirstOrDefault().InnerHtml;
                        if (!string.IsNullOrEmpty(strDate))
                        {
                            string toBeSearched = "on ";
                            string tempDt = strDate.Substring(strDate.IndexOf(toBeSearched) + toBeSearched.Length);
                            review.ReviewDate = Convert.ToDateTime(tempDt);
                        }
                        //Rating
                        var rt = item.Descendants(0).Where(n => n.HasClass("review-rating")).FirstOrDefault().InnerHtml;
                        rt = rt.Substring(rt.IndexOf('>') + 1);
                        int rtIndex = rt.IndexOf("<");
                        if (rtIndex > 0)
                        {
                            rt = rt.Substring(0, rtIndex);
                            string toBeSearched = " out";
                            string tempRt = rt.Substring(0, rt.IndexOf(toBeSearched));
                            review.Rating = Convert.ToInt32(Math.Round(Convert.ToDouble(tempRt)));
                        }
                        //Comment
                        var com = item.Descendants(0).Where(n => n.HasClass("review-text-content")).FirstOrDefault().InnerHtml;
                        com = com.Substring(com.IndexOf('>') + 1);
                        int comIndex = com.IndexOf("<");
                        if (comIndex > 0)
                            com = com.Substring(0, comIndex);
                        review.Comment = com;
                        //Subject
                        var sub = item.Descendants(0).Where(n => n.HasClass("review-title-content")).FirstOrDefault().InnerHtml;
                        sub = sub.Substring(sub.IndexOf('>') + 1);
                        int index = sub.IndexOf("<");
                        if (index > 0)
                            sub = sub.Substring(0, index);

                        review.Subject = sub;
                        lvReviews.Add(review);
                    }
                }
                _CustRepo.SaveCustomerReviews(lvReviews);
            }
            return Ok(lvReviews);
        }


    }
}