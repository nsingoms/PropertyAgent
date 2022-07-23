using AutoMapper.QueryableExtensions;

namespace PropertyAgent.Server.Services
{
    public class PropertyRepo : IPropertyInterface
    {
        private readonly PropertyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmployeeInterface _employeeInterface;

        public PropertyRepo(PropertyDbContext context,IMapper mapper,IEmployeeInterface employeeInterface)
        {
            _context = context;
            _mapper = mapper;
            _employeeInterface = employeeInterface;
        }
        public async Task<Property> AddProperty(Property property)
        {
           await _context.Properties.AddAsync(property);
           return property;
        }

        public async Task DeleteProperty(int id)
        {
            _context.Properties.Remove(await _context.Properties.Include(p=>p.Photos).FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<PropertyDto>> GetProperties()
        {
            return await _context.Properties
                         .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                         .ToListAsync();
        }

        public async Task<Property> GetProperty(int id)
        {
            return await _context.Properties
                .Include(a=>a.AppUser)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Property> UpdateProperty(Property property)
        {
            var entry = _context.Entry(property);
            entry.State = EntityState.Modified;

            return Task.FromResult(property);
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
