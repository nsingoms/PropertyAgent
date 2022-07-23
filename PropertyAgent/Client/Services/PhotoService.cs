using System.Net.Http.Json;

namespace PropertyAgent.Client.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _httpClient;

        public PhotoService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<string> UploadImage(MultipartFormDataContent contents)
        {
            var postResult = await _httpClient.PostAsJsonAsync("employee/add-Photo", contents);
            var postContent = await postResult.Content.ReadAsStringAsync();
            
            if (postResult.IsSuccessStatusCode)
            {
                var imgUrl = JsonSerializer.Deserialize<string>(postContent);
                return imgUrl;
            }
            else
            {
                throw new ApplicationException(postContent);
            }


        }
        public async Task<string> UploadPropertyImage(MultipartFormDataContent content)
        {
            var postResult = await _httpClient.PostAsync("upload/", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (postResult.IsSuccessStatusCode)
            {
               var ImgUrl = Path.Combine("https://localhost:7201/", postContent);
                return ImgUrl;
            }
            else
            {
                throw new ApplicationException(postContent);
            }


        }
     
    }
}
