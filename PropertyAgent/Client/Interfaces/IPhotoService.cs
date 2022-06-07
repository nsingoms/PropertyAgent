namespace PropertyAgent.Client.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadImage(MultipartFormDataContent content);
        Task<string> UploadPropertyImage(MultipartFormDataContent content);
    }
}
