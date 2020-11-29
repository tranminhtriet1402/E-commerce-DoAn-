using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebsiteBicycleStore
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "ActeHjPhCmH5EZcwMUFBupkA0SsX4D1EGAXxy-evSiKfOPkDSdKb546dbOP1Z6-Qsps3jcW7jUGeVa6z";
            clientSecret = "EFYxlf4o9zXFXJmoevyvQL1-s0giZgroQtOpA6YMoifxuMT9--1HpYVBgj3_r9y6_VrLMme0QpBQuzgi";
        }

        private static Dictionary<string,string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret,getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}