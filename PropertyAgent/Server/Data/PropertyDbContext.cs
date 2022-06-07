

namespace PropertyAgent.Server.Data
{
    public class PropertyDbContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options)
            : base(options)
        {
        }
       
        public DbSet<Property> Properties { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<SocialMedia> Socials { get; set; }
    }
}