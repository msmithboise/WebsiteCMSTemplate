//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteTemplateProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubPage
    {
        public int SubPageId { get; set; }
        public Nullable<int> PageId { get; set; }
        public string SubPageDescription { get; set; }
        public string PageDescription { get; set; }
        public Nullable<bool> IsPrivate { get; set; }
        public Nullable<int> WebContentId { get; set; }
    }
}