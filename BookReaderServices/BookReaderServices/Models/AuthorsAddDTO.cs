using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class AuthorsAddDTO
    {
        [DataMember(Name="name")]
        public string Name { get; set; }
        [DataMember(Name = "surname")]
        public string Surname { get; set; }
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
    }
}