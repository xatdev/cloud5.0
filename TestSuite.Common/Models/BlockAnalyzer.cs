using System;


namespace TestSuite.Common.Models
{
    public class BlockAnalyzer
    {
        public string BlockName { get; set; }
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public string Summary { get; set; }
    }
}