using System;
using System.Net.Http;
using System.Web.Http;
using WebAPIEnd2EndEncryption.Models;
using WebAPIEnd2EndEncryption.SHA512EncryptionEngine;

namespace WebAPIEnd2EndEncryption.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPayment()
        {
            return Request.CreateResponse(new
            {
                error = false,
                message = "Payments"
            });
        }
        [HttpPost]
        public HttpResponseMessage PostPayment([FromBody]Payment payment)
        {
            try
            {
                if (payment == null) return Request.CreateResponse(new
                {
                    error = true,
                    message = "Bad Request"
                });

                string rawValue = $"{payment.TotalAmount}{payment.Currency}{payment.TransReference}";
                string computeHash = EncryptionHelper.EncryptSha512(rawValue);
                if (payment.Hash.ToLower() != computeHash.ToLower()) return Request.CreateResponse(new
                {
                    error = true,
                    message = "Invalid hash detected"
                });

                return Request.CreateResponse(new
                {
                    error = false,
                    message = "Payment Processed successfully"
                });
            }
            catch (Exception)
            {
                return Request.CreateResponse(new
                {
                    error = true,
                    message = "Error occured while processing your request"
                });
            }
        }
    }
}
