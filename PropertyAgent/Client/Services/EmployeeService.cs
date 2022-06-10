using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PropertyAgent.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true ,
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
        }
        public async Task<EmployeeDto> GetEmployee(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"employee/{id}");
            var content = await httpResponse.Content.ReadAsStringAsync();
            
            if (httpResponse.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<EmployeeDto>(content, _options);
            }
            else
            {
                throw new Exception(content);
            }
           
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var httpResponse = await _httpClient.GetAsync("employee/");
            var content = await httpResponse.Content.ReadAsStringAsync();

            if(httpResponse.IsSuccessStatusCode== false)
            {
                throw new ApplicationException(content);
            }

            var employees = JsonSerializer.Deserialize<IEnumerable<EmployeeDto>>(content, _options);
            return employees;
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employee)
        {
             await _httpClient.PutAsJsonAsync<EmployeeUpdateDto>("employee/",employee);
        }
    }
}
