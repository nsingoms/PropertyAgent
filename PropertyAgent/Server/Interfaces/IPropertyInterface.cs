namespace PropertyAgent.Server.Interfaces
{
    public interface IPropertyInterface
    {
        Task<Property> AddProperty(Property property);
        Task<Property> GetProperty(int id);
        Task<IEnumerable<PropertyDto>> GetProperties();
        Task DeleteProperty(int id);
        Task<Property>UpdateProperty(Property property);
        Task<bool> Complete();
    }
}
