//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InputsModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shelves
    {
        public Shelves()
        {
            this.Books = new HashSet<Books>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<Books> Books { get; set; }
    }
}
