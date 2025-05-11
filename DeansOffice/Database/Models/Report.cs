using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public string ReportType { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string CreatedBy { get; set; }
        public string FilePath { get; set; }
    }

}
