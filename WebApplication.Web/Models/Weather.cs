﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }

        public int FiveDay { get; set; }

        public int Low { get; set; }

        public int High { get; set; }

        public string Forecast { get; set; }

    }
}