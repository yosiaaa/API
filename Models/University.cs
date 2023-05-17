namespace API.Models
{
    public class University
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
