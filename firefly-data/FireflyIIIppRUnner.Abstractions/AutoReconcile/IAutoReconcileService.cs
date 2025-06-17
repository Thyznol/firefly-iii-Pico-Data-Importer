using FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile
{
    public interface IAutoReconcileService
    {
        public Task<AutoReconcileStatus> DryRun(AutoReconcileRequestDto dto);
        public Task<AutoReconcileDryRunResponseDto> GetDryRunResult();
        public Task<AutoReconcileStatus> Run(AutoReconcileRequestDto dto);
        public AutoReconcileStatus GetStatus();
        public AutoReconcileStatus Stop();
    }
}
