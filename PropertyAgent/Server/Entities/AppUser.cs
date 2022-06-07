namespace PropertyAgent.Server.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
          
            Socials = new List<SocialMedia>();
            Properties = new List<Property>();  
        }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePic { get; set; } 
        public virtual ICollection<SocialMedia> Socials { get; set; } 
        public virtual ICollection<Property> Properties { get; set; }
       

    }
}
