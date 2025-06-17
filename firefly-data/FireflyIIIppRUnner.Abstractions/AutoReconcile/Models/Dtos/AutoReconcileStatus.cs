using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Dtos
{
    public class AutoReconcileStatus
    {
        public AutoReconcileState State { get; set; } = AutoReconcileState.Completed;
        public int TotalTransfers { get; set; }
        public int TotalSourceTransactions { get; set; }
        public int TotalDestinationTransactions { get; set; }
        public int CompletedTransfers { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AutoReconcileState
    {
        [EnumMember(Value = "completed")]
        Completed,
        [EnumMember(Value = "failed")]
        Failed,
        [EnumMember(Value = "stopped")]
        Stopped,
        [EnumMember(Value = "getting-transactions")]
        GettingTransactions,
        [EnumMember(Value = "pairing")]
        PairingTransactions,
        [EnumMember(Value = "running")]
        Running
    }
}
