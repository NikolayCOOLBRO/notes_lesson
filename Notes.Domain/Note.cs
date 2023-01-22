namespace Notes.Domain
{
    public class Note
    {
        public Guid UserId { get; set; }
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditTime { get; set; }
    }
}
