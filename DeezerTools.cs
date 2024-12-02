using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace NameThatTune
{
    public static class DeezerTools
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetPreviewUrlAsync(string trackName, string artistName)
        {
            string url = $"https://api.deezer.com/search?q=track:\"{trackName}\" artist:\"{artistName}\"";
            var response = await client.GetStringAsync(url);
            File.WriteAllText("new.txt", response);
            
            //Due to the changes in Spotify API and lack of a popular Deezer APi library for C#, I had to learn
            //quickly to bring functionality back to the app. I used online learning resources (Microsoft
            //.NET documentation, Stack Overflow) in conjuction with some questions to AI models in order to
            // write this code.

            
            using (JsonDocument doc = JsonDocument.Parse(response))
            {

                if (doc.RootElement.TryGetProperty("data", out JsonElement data) && data.GetArrayLength() > 0)
                {
                   
                    var firstResult = data[0];

                    if (firstResult.TryGetProperty("artist", out JsonElement artistElement) && artistElement.TryGetProperty("name", out JsonElement artistNameElement))
                    {
                        //verifies artist is correct before loading URL, in case of bad Deezer search - which created some pretty hilarious)
                        //results when left alone. going to refine this in later versions
                        string deezerArtistName = artistNameElement.GetString();
                 
                        if (deezerArtistName != artistName) return null;
                    }

                    // Try to get the "preview" property safely
                    if (firstResult.TryGetProperty("preview", out JsonElement previewUrlElement))
                    {
                        string previewUrl = previewUrlElement.GetString();
                        
                        return previewUrl;
                    }
                    else
                    {
                        
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
