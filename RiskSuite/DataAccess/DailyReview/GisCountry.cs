﻿using LogSuite.DataAccess.References;
using System.Collections.Generic;

namespace LogSuite.DataAccess.DailyReview
{
    public class GisCountry
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsHidden { get; set; }
        public List<GisCountryResource> Resources { get; set; } = new List<GisCountryResource>();
        public int GisId { get; set; }
        public Gis Gis { get; set; }
        public List<GisCountryValue> Values { get; set; } = new List<GisCountryValue>();
    }
}
