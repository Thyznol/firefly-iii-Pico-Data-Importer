using FireflyIIIpp.Core.Models;
using FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Dtos
{
    public class AutoReconcileRequestDto
    {
        public List<RunnerQueryOperation> SourceQueryOperations { get; set; } = new List<RunnerQueryOperation>();
        public List<RunnerQueryOperation> DestinationQueryOperations { get; set; } = new List<RunnerQueryOperation>();
        public AutoReconcilePairingStrategyDto PairingStrategy { get; set; } = new AutoReconcilePairingStrategyDto();
        public AutoReconcileJoiningStrategyDto JoiningStrategy { get; set; } = new AutoReconcileJoiningStrategyDto();

    }

    public class AutoReconcilePairingStrategyDto
    {
        public bool RequireMatchingDescriptions { get; set; }
        public bool RequireMatchingDates { get; set; }
        public int DateMatchToleranceInDays { get; set; } = 0;
    }

    public class AutoReconcileJoiningStrategyDto
    {
        public JoiningStrategyEnum DescriptionJoinStrategy { get; set; } = JoiningStrategyEnum.Concatenate;
        public JoiningStrategyEnum DateJoinStrategy { get; set; } = JoiningStrategyEnum.Average;
        public JoiningStrategyEnum CategoryJoinStrategy { get; set; } = JoiningStrategyEnum.Clear;
        public JoiningStrategyEnum NotesJoinStrategy { get; set; } = JoiningStrategyEnum.Concatenate;
    }
}
