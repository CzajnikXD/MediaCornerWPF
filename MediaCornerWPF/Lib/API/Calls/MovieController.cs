using MediaCornerWPF.Lib.API.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaCornerWPF.Lib.API.Calls
{
    internal class MovieController
    {
        public static async Task<List<MovieModel>> GetPopular()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1"),
                Headers =
    {
        { "accept", "application/json" },
        { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmNzVhMjY4ZDkxZDk1YjczOGFkODEyOGY2NTNjMzNmNSIsInN1YiI6IjY2NjQ2NmUyODYwMjJkOWQ5OTc4Yzg4MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.MPwar435YWATMewdq9Fwm_crr_oRjjV9aB69ZwFL7VY" },
    },
            };

            using (var response = await ApiController.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var movielist = JObject.Parse(result)["results"].ToString();

                    List<MovieModel> ret = JsonConvert.DeserializeObject<List<MovieModel>>(movielist);

                    return ret;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<MovieModel> GetMovie(int id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{id}?language=en-US"),
                Headers =
    {
        { "accept", "application/json" },
        { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmNzVhMjY4ZDkxZDk1YjczOGFkODEyOGY2NTNjMzNmNSIsInN1YiI6IjY2NjQ2NmUyODYwMjJkOWQ5OTc4Yzg4MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.MPwar435YWATMewdq9Fwm_crr_oRjjV9aB69ZwFL7VY" },
    },
            };

            using (var response = await ApiController.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    MovieModel ret = JObject.Parse(result).ToObject<MovieModel>();

                    return ret;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<MovieModel>> SearchMovie(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US&page=1"),
                Headers =
    {
        { "accept", "application/json" },
        { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmNzVhMjY4ZDkxZDk1YjczOGFkODEyOGY2NTNjMzNmNSIsInN1YiI6IjY2NjQ2NmUyODYwMjJkOWQ5OTc4Yzg4MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.MPwar435YWATMewdq9Fwm_crr_oRjjV9aB69ZwFL7VY" },
    },
            };

            using (var response = await ApiController.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var movielist = JObject.Parse(result)["results"].ToString();

                    List<MovieModel> ret = JsonConvert.DeserializeObject<List<MovieModel>>(movielist);

                    return ret;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

