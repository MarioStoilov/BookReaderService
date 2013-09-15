using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BookReaderServices.Models
{
    [DataContract]
    public class BookToShelfDTO
    {
        [DataMember(Name="bookId")]
        public int BookId { get; set; }
        [DataMember(Name = "shelfId")]
        public int ShelfId { get; set; }
    }
}