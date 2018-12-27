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

        public async Task AddSingleExpense(SingleExpense singleExpense)
        {
            var json = JsonConvert.SerializeObject(singleExpense);
            var url = $"{serverAddress}/User/AddSingleExpense";
            var headers = new Dictionary<string, string>
            {
                { "singleExpense", json }
            };
            var content = new FormUrlEncodedContent(headers);
            var response = await httpClient.PostAsync(url, content);
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
        
        public async Task<bool> CreateNewUserIfNotExists(Channel channel)
        {
            var userJson = JsonConvert.SerializeObject(channel.User);
            var channelJson = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}/Security/CreateNewUser";
            var headers = new Dictionary<string, string>
            {
                { "user", userJson },
                { "channel", channelJson }
            };
            var content = new FormUrlEncodedContent(headers);
            var response = await httpClient.PostAsync(url, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<List<string>> GetRegularCategories(Channel channel)
        {
            var channelJson = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}/User/GetRegularExpensesCategories?channel={channelJson}";
            var response = await httpClient.GetAsync(url);
            var resultJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(resultJson);
        }

        public async Task RegisterChannelIfNotExists(Channel channel)
        {
            var json = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}/Security/AddChannelIfNotExists";
            var headers = new Dictionary<string, string>
            {
                { "channel", json }
            };
            var content = new FormUrlEncodedContent(headers);
            await httpClient.PostAsync(url, content);
        }
    }
}
