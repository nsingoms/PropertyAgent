namespace PropertyAgent.Client.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployee(string id);
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task UpdateEmployee(EmployeeUpdateDto employee);
    }
}
