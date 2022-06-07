namespace PropertyAgent.Server.Entities
{
    public class Property
    {
        public Property()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Location { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<Photo> Photos { get; set; } 

    }
}
