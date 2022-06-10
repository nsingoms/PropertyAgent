
namespace PropertyAgent.Server.Services
{
    public class EmployeeRepo : IEmployeeInterface
    {
        private readonly PropertyDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepo(PropertyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public async Task<EmployeeDto> GetEmployee(string id)
        {
            var employee = await _context.Users
                .Include(p=>p.Properties)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee;
        }
        public async Task<AppUser> GetUser(string id)
        {
            var user = await _context.Users
                         .FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return await _context.Users
                       .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                       .ToListAsync();


        }

        public void UpdateEmployee(AppUser employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
