using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class ShelvesDTO
    {
        [DataMember(Name="title")]
        public string Title { get; set; }
    }
}