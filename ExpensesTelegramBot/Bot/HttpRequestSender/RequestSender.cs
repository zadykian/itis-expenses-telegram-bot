using System;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Bot
{
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient httpClient;
        private readonly string serverAddress;

        public RequestSender(string serverAddress)
        {
            httpClient = new HttpClient();
            this.serverAddress = serverAddress;
        }

        public async Task<bool> CheckIfUserExists(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var url = $"{serverAddress}/Security/CheckIfUserExists?user={userJson}";
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateNewUserIfNotExists(User user, Channel channel)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var channelJson = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}/Security/CreateNewUser";
            var values = new Dictionary<string, string>
            {
                { "user", userJson },
                { "channel", channelJson }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await httpClient.PostAsync(url, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public void RegisterChannelIfNotExists(Channel channel)
        {
            throw new NotImplementedException();
        }
    }
}
