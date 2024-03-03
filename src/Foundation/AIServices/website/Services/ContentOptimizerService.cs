using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Fields;
using SitecoreRedemption.Foundation.AIServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreRedemption.Foundation.AIServices.Services
{
    public class ContentOptimizerService : IContentOptimizerService
    {
        private readonly HttpClient _httpClient;

        public ContentOptimizerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<OpenAiResponse> OptimizeContentAsync(string prompt, string settingId)
        {
            var dbName = "master";
            if (!string.IsNullOrEmpty(settingId) && ID.IsID(settingId) && !string.IsNullOrEmpty(dbName))
            {
                var db = Sitecore.Configuration.Factory.GetDatabase(dbName);
                if (db == null) { return null; }

                var settingItem = db.GetItem(settingId);
                if (settingItem == null) { return null; }

                Int32.TryParse(settingItem[Templates.AIServiceSettings.Fields.NumberOfChoices], out int numberOfChoices);
                Int32.TryParse(settingItem[Templates.AIServiceSettings.Fields.MaxTokens], out int maxTokens);
                Double.TryParse(settingItem[Templates.AIServiceSettings.Fields.Temperature], out double temperature);
                Double.TryParse(settingItem[Templates.AIServiceSettings.Fields.TopP], out double topP);
                Double.TryParse(settingItem[Templates.AIServiceSettings.Fields.FrequencyPenalty], out double frequencyPenalty);
                Double.TryParse(settingItem[Templates.AIServiceSettings.Fields.PresencePenalty], out double presencePenalty);

                var instructionMessage = new Message
                {
                    Content = settingItem[Templates.AIServiceSettings.Fields.AIInstructions],
                    Role = RoleType.System
                };

                var promptMessage = new Message
                {
                    Content = prompt,
                    Role = RoleType.User
                };

                var messages = new List<Message> { instructionMessage, promptMessage };
                var requestBody = new OpenAiRequest
                {
                    Model = settingItem[Templates.AIServiceSettings.Fields.Model],   //"gpt-4-turbo-preview"
                    Messages = messages,
                    Temperature = temperature > 0 ? temperature : 0.7,
                    MaxTokens = maxTokens > 0 ? maxTokens : 256,
                    TopP = topP >= 0 ? topP : 0,
                    FrequencyPenalty = frequencyPenalty >= 0 ? frequencyPenalty : 0,
                    PresencePenalty = presencePenalty >= 0 ? presencePenalty : 0,
                    NumChoicesPerMessage = numberOfChoices > 0 ? numberOfChoices : 1,
                    ResponseFormat = new ResponseFormat { Type = "json_object" }
                };

                ReferenceField apiConfiguration = settingItem.Fields[Templates.AIServiceSettings.Fields.APIConfiguration];

                var apiConfigurationItem = apiConfiguration.TargetItem;
                if (apiConfigurationItem == null) { return null; }

                var apiUrl = apiConfigurationItem[Templates.APIConfiguration.Fields.APIEndpoint];
                var apiKey = apiConfigurationItem[Templates.APIConfiguration.Fields.APIKey];

                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl)) { return null; }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                //looks like OpenAI sometimes injects random characters when ouptutting JSON. Remove those so we can derserialize properly:
                responseString = responseString.Replace("```json\\n{", "{").Replace("}\\n```", "}");

                var settings = new JsonSerializerSettings { Error = (se, ev) => { ev.ErrorContext.Handled = true; } };
                var responseObject = JsonConvert.DeserializeObject<OpenAiResponse>(responseString, settings);

                return responseObject;
            }
            return null;
        }


    }
}
