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
    
    public partial class Exam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exam()
        {
            this.ExamResults = new HashSet<ExamResult>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamResult> ExamResults { get; set; }
    }
}
