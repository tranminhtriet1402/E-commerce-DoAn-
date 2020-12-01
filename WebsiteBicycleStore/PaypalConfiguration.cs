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
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = "ActeHjPhCmH5EZcwMUFBupkA0SsX4D1EGAXxy-evSiKfOPkDSdKb546dbOP1Z6-Qsps3jcW7jUGeVa6z";
            ClientSecret = "ELvj_-30s8K0p2IG_prnZG8CpBHgbTLwQlmIk111TsuAXfxysts2tAflT1eXM2zmRtB7UPNWxGkE_d_E";
        }

        private static Dictionary<string,string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret,GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = GetConfig();
            return apicontext;
        }
    }
}