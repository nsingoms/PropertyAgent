namespace PropertyAgent.Client.Interfaces
{
    public interface IProperty
    {
        Task CreatePropertyAsync(PropertyDto propertyDto);
        Task<IEnumerable<PropertyDto>> GetPropertiesAsync();
        Task<PropertyDto> GetPropertyAsync(int id);
        Task DeletePropertyAsync(int id);
        Task UpdatePropertyAsync(PropertyDto propertyDto);
    }
}
