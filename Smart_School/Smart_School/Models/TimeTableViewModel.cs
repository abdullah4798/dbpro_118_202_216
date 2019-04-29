using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smart_School.Models
{
    public class TimeTableViewModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SectionId { get; set; }

        public int ClassId { get; set; }

        public DateTime Date { get; set; }
    }
}