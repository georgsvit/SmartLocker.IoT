using Newtonsoft.Json;
using SmartLocker.IoT.Contracts;
using SmartLocker.IoT.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Services
{
    class HttpClientService
    {
        private HttpClient client;

        public HttpClientService(int timeout)
        {
            client = new();
            client.Timeout = TimeSpan.FromSeconds(timeout);
        }

        public async Task<bool> SendViolationRegisterNote(ViolationNotePostRequest note) 
        {
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ApiRoutes.ViolationPost, data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> TakeTool(TakeToolRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ApiRoutes.TakeTool, data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ReturnTool(ReturnToolRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ApiRoutes.ReturnTool, data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> GetConfig(Guid id)
        {
            var response = await client.GetAsync(ApiRoutes.GetLockersConfig + $"/{id}");

            var content = await response.Content.ReadAsStringAsync();

            bool isBlocked = JsonConvert.DeserializeObject<bool>(content);

            return isBlocked;
        }
    }
}
