using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    public class BooksPutModelDTO
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "authorId")]
        public int AuthorId { get; set; }
    }
}