namespace PropertyAgent.Shared.Dtos
{
    public class PropertyDto
    {
        public PropertyDto()
        {
            Photos = new List<PhotoDto>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<PhotoDto> Photos { get; set; } 
    }
}
