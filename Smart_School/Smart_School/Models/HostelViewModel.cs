﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smart_School.Models
{
    public class HostelViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }


        public int Rent { get; set; }
        [Display(Name = "Hostel Name")]
        public string HostelName { get; set; }

        [Display(Name = "Hostel Location")]
        public string HostelLocation { get; set; }


    }
}