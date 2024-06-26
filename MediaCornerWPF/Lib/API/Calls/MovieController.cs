﻿using MediaCornerWPF.Lib.API.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaCornerWPF.Lib.API.Calls
{
    internal class MovieController
    {
        /// <summary>
        /// Funkcja pobierająca listę popularnych filmów z The Movie Database API.
        /// </summary>
        /// <returns>
        ///     Lista obiektów MovieModel reprezentujących popularne filmy.
        /// </returns>
        /// <exception cref="Exception">
        ///     Rzuca wyjątek w przypadku nieudanej próby pobrania danych.
        /// </exception>
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

        /// <summary>
        /// Funkcja pobierająca szczegóły filmu na podstawie jego identyfikatora z The Movie Database API.
        /// </summary>
        /// <param name="id">
        ///     Identyfikator filmu, którego szczegóły mają zostać pobrane.
        /// </param>
        /// <returns>
        ///     Obiekt MovieModel reprezentujący szczegóły filmu.
        /// </returns>
        /// <exception cref="Exception">
        ///     Rzuca wyjątek w przypadku nieudanej próby pobrania danych.
        /// </exception>
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

        /// <summary>
        /// Funkcja wyszukująca filmy na podstawie podanego zapytania w The Movie Database API.
        /// </summary>
        /// <param name="query">
        ///     Zapytanie wyszukiwane.
        /// </param>
        /// <returns>
        ///     Lista obiektów MovieModel reprezentujących wyniki wyszukiwania.
        /// </returns>
        /// <exception cref="Exception">
        ///     Rzuca wyjątek w przypadku nieudanej próby pobrania danych.
        /// </exception>
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
