namespace PropertyAgent.Client.Services
{
    public class PropertyService : IProperty
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PropertyService> _logger;
        private readonly JsonSerializerOptions _options;

        public PropertyService(HttpClient httpClient, ILogger<PropertyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }



        public async Task CreatePropertyAsync(PropertyDto property)
        {
            var content = JsonSerializer.Serialize(property);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("property", bodyContent);

            if (response.IsSuccessStatusCode)
            {

            }
           
    }

        public async Task<PropertyDto> GetPropertyAsync(int id)
        {
            var response = await _httpClient.GetAsync($"property/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
             var property= JsonSerializer.Deserialize<PropertyDto>(result, _options);
            return property;
        }

        public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync()
        {
            var response = await _httpClient.GetAsync("property");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var properties=  JsonSerializer.Deserialize<IEnumerable<PropertyDto>>(result, _options);

            return properties;
        }

        public async Task DeletePropertyAsync(int id)
        {
            await _httpClient.DeleteAsync($"property/{id}");
           
        }

        public async Task UpdatePropertyAsync(PropertyDto propertyDto)
        {
            var content = JsonSerializer.Serialize(propertyDto);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"property/",bodyContent);
            response.EnsureSuccessStatusCode();
        
           
        }
    }
}