using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreRedemption.Foundation.AIServices.Models
{
    public class OpenAiResponse
    {
        [JsonProperty("choices", NullValueHandling = NullValueHandling.Ignore)]
        public List<Choice> Choices { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public int? Created { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
        public string Object { get; set; }

        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public Usage Usage { get; set; }
    }


    public class Choice
    {
        [JsonProperty("finish_reason", NullValueHandling = NullValueHandling.Ignore)]
        public string FinishReason { get; set; }

        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public int? Index { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public ScoredMessage Message { get; set; }

        [JsonProperty("logprobs", NullValueHandling = NullValueHandling.Ignore)]
        public object Logprobs { get; set; }
    }

    public class ScoredMessage
    {
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))] // This will convert between string and enum when serializing/deserializing
        public RoleType Role { get; set; }

        [JsonConverter(typeof(NestedJsonConverter))]
        public ScoredContent Content { get; set; }
    }

    public class ScoredContent
    {
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; } 
        
        [JsonProperty("seo_score", NullValueHandling = NullValueHandling.Ignore)]
        public int SeoScore { get; set; }

        [JsonProperty("readability", NullValueHandling = NullValueHandling.Ignore)]
        public int Readability { get; set; }

        [JsonProperty("accessibility", NullValueHandling = NullValueHandling.Ignore)]
        public int Accessibility { get; set; }

        [JsonIgnore]
        public string FixedHtml { get; set; }
    }

    public class Usage
    {
        [JsonProperty("completion_tokens", NullValueHandling = NullValueHandling.Ignore)]
        public int? CompletionTokens { get; set; }

        [JsonProperty("prompt_tokens", NullValueHandling = NullValueHandling.Ignore)]
        public int? PromptTokens { get; set; }

        [JsonProperty("total_tokens", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalTokens { get; set; }
    }
}
