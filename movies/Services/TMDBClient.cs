using movies.Models;
using System.Net.Http.Json;

namespace movies.Services
{
    public class TMDBClient
    {
        private readonly HttpClient _httpclient;
        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            _httpclient = httpClient;
            _httpclient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _httpclient.DefaultRequestHeaders.Add("Accept", "application/json");

            string apiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIzYzNjYjA4YzA1YjIyZDUyZjNiYWZkM2RhYmFhMjQyNSIsInN1YiI6IjY1NzYwNTc5YTFkMzMyMDBlMWI3OWRkYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.kEpgQ5mRhg1J5Oua4IEXrFL4JIFp4kUAY3ucaLFsPZ4";
            _httpclient.DefaultRequestHeaders.Authorization = new ("Bearer", apiKey);   
        }

        public Task<PopularMoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpclient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/popular?page={page}");
        }

        public Task<MovieDetails?> GetMovieDetails(int id)
        {
            return _httpclient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }
    }
}
