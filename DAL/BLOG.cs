//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BLOG
    {
        public BLOG()
        {
            this.THREADs = new HashSet<THREAD>();
        }
    
        public long BLOG_ID { get; set; }
        public long FAC_ID { get; set; }
        public string BLOG_NAME { get; set; }
    
        public virtual FACULTY FACULTY { get; set; }
        public virtual ICollection<THREAD> THREADs { get; set; }
    }
}
