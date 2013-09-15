using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class ShelvesGetAllDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
        [DataMember(Name="title")]
        public string Title { get; set; }
        [DataMember(Name="userId")]
        public int UserId { get; set; }
    }
}