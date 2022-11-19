using System;

namespace BigProjectOne.Libraries.Models.Interfaces
{
    public class Audit
    {
        public String User { get; set; }
        public DateTime? Date { get; set; }
    }
}