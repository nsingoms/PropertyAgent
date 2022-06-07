namespace PropertyAgent.Server.Interfaces
{
    public interface IEmployeeInterface
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployee(string id);
        Task<AppUser> GetUser(string id);
        void UpdateEmployee(AppUser employee);
        Task<bool> Complete();


    }
}
