using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BookReaderServices.Models
{

        [DataContract]
        public class CategoriesDTO
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }
        }
    
}