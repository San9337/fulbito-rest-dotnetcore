using apidata.DataContracts.External;
using apidata.Mapping;
using FulbitoRest.Helpers.Contracts;
using model.Business.Structures;
using model.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FulbitoRest.Helpers.Implementations
{
    /// <summary>
    /// Does the communication with external systems
    /// </summary>
    public class ThirdPartyHelper : IThirdPartyHelper
    {
        public async Task<FacebookUser> GetFacebookUser(string fbToken)
        {
            //$fields = 'id,email,first_name,last_name,link,name';
            var path = "https://graph.facebook.com/me?access_token=" + fbToken + "&fields=email,name,first_name,last_name";
            var uri = new Uri(path);

            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                throw new FulbitoException("Facebook rejected the request (" + path + ")\n" + contentError);
            }

            var content = await response.Content.ReadAsStringAsync();
            var fbUser = JsonConvert.DeserializeObject<FacebookUserViewModel>(content);

            return fbUser.Map(fbToken);
        }
    }
}
