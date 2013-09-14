using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class CommentsAddDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
        [DataMember(Name = "bookId")]
        public int BookId { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "info")]
        public string Info { get; set; }
    }
}