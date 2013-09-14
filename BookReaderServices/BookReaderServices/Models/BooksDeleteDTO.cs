using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class BooksDeleteDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
    }
}