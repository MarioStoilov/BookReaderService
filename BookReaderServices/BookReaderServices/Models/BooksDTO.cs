using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class BooksDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
         [DataMember(Name = "title")]
        public string Title { get; set; }
         [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "authorInfo")]
         public AuthorByBookDTO AuthorInfo { get; set; }
        [DataMember(Name = "rating")]
        public int Rating { get; set; }
        [DataMember(Name = "categoryDetails")]
        public string CategoryDetails { get; set; }
    }
}