using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class AuthorByBookDTO
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
    }
}