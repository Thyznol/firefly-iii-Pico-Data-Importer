using FireflyIIIppRunner.Abstractions.AutoReconcile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Dtos
{
    public class AutoReconcileDryRunResponseDto
    {
        public List<AutoReconcileTransfer> Transfers { get; set; } = new List<AutoReconcileTransfer>();
    }
}
