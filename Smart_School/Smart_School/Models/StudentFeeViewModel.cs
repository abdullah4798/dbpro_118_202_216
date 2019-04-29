﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smart_School.Models
{
    public class StudentFeeViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id")]
        public int StudentId { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Display(Name = "`Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}