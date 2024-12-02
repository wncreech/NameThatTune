using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using SpotifyAPI.Web;

//this should be in .gitignore if I did this correctly//

namespace NameThatTune
{
    public static class Authenticator
    {
        private static string clientId = "ff84f48ed47340758c56a52c8759867c";

        private static string clientSecret = clientSecret = "102768f7f07a4c219a37a2b9f90914b0";

        private static string refreshToken = "AQCWUPCgZAx1Hq_1cZk3NDDJPjjjT9rk--4hjHEJ8QUvW3NsFRquy0qXDFSsT8QqRwjF3AWdoRim16jvRD6K5sT5BNFxlX_5j9fqCrNFVO7jsS8Xma653EzhRe2kApDJ_D4";

        public static bool isActive = false;

        public static async Task<SpotifyClient> Run()
        {
            var accessToken = await GetAccessToken();
            var config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
            var spotify = new SpotifyClient(config); //refresh using own spotify as as of now no personal info needed
            Activate();
            return spotify;
        }

        private static async Task<string> GetAccessToken() 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Add("Authorization", $"Basic {credentials}");

            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            });

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var json = JsonDocument.Parse(responseBody);
                return json.RootElement.GetProperty("access_token").GetString();
            }

            throw new Exception("Failed to refresh access token: " + responseBody);
        }

        public static void Activate()
        {
            isActive = true;
        }

        public static void Deactivate()
        {
            isActive = false;
        }
    }
}