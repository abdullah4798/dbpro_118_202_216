//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Smart_School
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enrollment
    {
        public int id { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Section Section { get; set; }
        public virtual Student Student { get; set; }
    }
}
