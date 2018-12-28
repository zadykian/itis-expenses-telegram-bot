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
        
        public async Task SetCategoriesList(string channelId, string categories)
        {
            var categoriesList = new CategoriesList(channelId, categories.Split(' ').ToList());
            var json = JsonConvert.SerializeObject(categoriesList);
            var url = $"{serverAddress}User/SetCategoriesList?categoriesList={json}";
            await httpClient.GetAsync(url);
        }

        public async Task AddSingleExpense(SingleExpense singleExpense)
        {
            var json = JsonConvert.SerializeObject(singleExpense);
            var url = $"{serverAddress}User/AddSingleExpense?singleExpense={json}";
            await httpClient.GetAsync(url);
        }

        public async Task<bool> CheckIfUserExists(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var url = $"{serverAddress}Security/CheckIfUserExists?user={json}";
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> CreateNewUserIfNotExists(Channel channel)
        {
            var json = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}Security/CreateNewUser?channel={json}";
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<List<string>> GetRegularCategories(Channel channel)
        {
            var json = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}User/GetRegularExpensesCategories?channel={json}";
            var response = await httpClient.GetAsync(url);
            var resultJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(resultJson);
        }

        public async Task RegisterChannelIfNotExists(Channel channel)
        {
            var json = JsonConvert.SerializeObject(channel);
            var url = $"{serverAddress}Security/AddChannelIfNotExists?channel={json}";
            await httpClient.GetAsync(url);
        }
    }
}
