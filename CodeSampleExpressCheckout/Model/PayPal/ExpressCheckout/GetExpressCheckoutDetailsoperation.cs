using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSampleExpressCheckout.Model.PayPal.Enum;

namespace CodeSampleExpressCheckout.Model.PayPal.ExpressCheckout
{
    public class GetExpressCheckoutDetailsOperation : ExpressCheckoutApi.Operation
    {
        public GetExpressCheckoutDetailsOperation(ExpressCheckoutApi ec, string token)
            : base(ec)
        {
            RequestNVP.Method = "GetExpressCheckoutDetails";
            Token = token;
        }
    }
}
