using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace showcase
{
    public interface IAlbumRepository
    {
        Task<List<PhotoAlbum>> getAlbum(long albumId);
    }

    public class AlbumRepository : IAlbumRepository
    {
        readonly HttpClient client;
        readonly Uri baseAddress = new Uri("https://jsonplaceholder.typicode.com/photos");

        public AlbumRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public Task<List<PhotoAlbum>> getAlbum(long albumId)
        {
            return client.GetFromJsonAsync<List<PhotoAlbum>>($"?albumId={albumId}");
        }
    }
}