﻿using System.ComponentModel.DataAnnotations;
using System;

namespace Rkoort_Marathon.Models
{
    public class Award
    {
        public int id { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
        public double time { get; set; }
    }
}
