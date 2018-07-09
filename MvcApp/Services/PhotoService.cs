using MvcApp.Controllers;
using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly string albumApiUrl = "https://jsonplaceholder.typicode.com/albums";
        private readonly string photoApiUrl = "https://jsonplaceholder.typicode.com/photos";

        public async Task<IEnumerable<DataTable>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(albumApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage albumResponse = await client.GetAsync(albumApiUrl);
                HttpResponseMessage photoResponse = await client.GetAsync(photoApiUrl);
                if (albumResponse.IsSuccessStatusCode && photoResponse.IsSuccessStatusCode)
                {
                    var albumData = await albumResponse.Content.ReadAsStringAsync();
                    var photoData = await photoResponse.Content.ReadAsStringAsync();

                    var albumResult = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Album>>(albumData);
                    var photoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Photo>>(photoData);

                    return photoResult.Join(albumResult, p => p.AlbumId, a => a.Id,
                        (photos, album) => new DataTable
                        {
                            AlbumName = album.Title,
                            PhotoTitle = photos.Title,
                            Url = photos.Url,
                            Thumbnail = photos.ThumbnailUrl
                        });
                }
            }
            return null;
        }

    }
}
