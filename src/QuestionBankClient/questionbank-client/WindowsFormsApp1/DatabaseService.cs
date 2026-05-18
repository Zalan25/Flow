using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuestionBankClient
{
    public static class DatabaseService
    {
        // Ez intézi a hálózati kommunikációt
        public static readonly HttpClient Client = new HttpClient();

        static DatabaseService()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            Client.BaseAddress = new Uri("https://89b8t8bl-7099.eun1.devtunnels.ms/");
        }

        public static async Task<bool> IsConnectionWorkingAsync()
        {
            try
            {
                // Megkérdezzük az API-t, hogy ő látja-e az adatbázist
                HttpResponseMessage response = await Client.GetAsync("api/quiz/test-connection");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}