using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreRedemption.Foundation.AIServices.Models
{
    public class Message
    {
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))] // This will convert between string and enum when serializing/deserializing
        public RoleType Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public enum RoleType
    {
        [EnumMember(Value = "system")]
        System,
        [EnumMember(Value = "user")]
        User,
        [EnumMember(Value = "assistant")]
        Assistant
    }
}
