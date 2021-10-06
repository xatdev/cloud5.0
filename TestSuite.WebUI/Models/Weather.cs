using System;
using System.ComponentModel.DataAnnotations;

namespace TestSuite.WebUI.Models
{
    public class Weather
    {
        public string date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; }
    }
}