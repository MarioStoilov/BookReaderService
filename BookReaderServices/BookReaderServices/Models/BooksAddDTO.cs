using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class BooksAddDTO
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "authorId")]
        public int AuthorId { get; set; }

        public int CategoryId { get; set; }
    }
}