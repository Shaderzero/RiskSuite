﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models.References
{
    public class CountryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter country name in russian")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter country name in english")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "Please enter country short name")]
        [StringLength(2, ErrorMessage = "Country short name must be 2 letter")]
        public string ShortName { get; set; }
        public string Ticker { get; set; }
        public IList<CountryNameDTO> Names { get; set; } = new List<CountryNameDTO>();
    }
}
