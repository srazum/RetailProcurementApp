using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RetailProcurement.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RetailProcurement.UnitTests
{
    internal static class ClientConfiguration
    {
        public static void ConfigureClient(this HttpClient client)
        {

            client.BaseAddress = new Uri("http://localhost:5000");
            var token = GetToken(client);
            token!.Wait();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Result);
        }

        private async static Task<string>? GetToken(HttpClient client)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Settings>()
                .Build();
            var username = configuration.GetValue<string>("Settings:USERNAME");
            var password = configuration.GetValue<string>("Settings:PASSWORD");
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UserDto { UserName = username, Password = password }));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var login = await client.PostAsync($"api/Login", byteContent);
            return await login.Content.ReadAsStringAsync();
        }
    }
}
