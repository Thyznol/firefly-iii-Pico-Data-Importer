using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JoiningStrategyEnum
    {
        [EnumMember(Value = "source")]
        Source,
        [EnumMember(Value = "destination")]
        Destination,
        [EnumMember(Value = "concatenate")]
        Concatenate,
        [EnumMember(Value = "average")]
        Average,
        [EnumMember(Value = "clear")]
        Clear
    }
}
