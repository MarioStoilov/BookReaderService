using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class CommentsByBookDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "info")]
        public string Info { get; set; }
    }
}