using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile.Models
{
    public class AutoReconcileTransfer
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
        public string Warning { get; set; }
    }
}
