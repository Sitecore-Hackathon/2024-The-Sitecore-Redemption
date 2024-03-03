using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace SitecoreRedemption.Foundation.AIServices
{
  public struct Templates
  {
        public struct AIServiceSettings
        {
            public static readonly ID ID = new ID("{1B70D039-4484-4C72-849F-AE027D72638A}");

            public struct Fields
            {
                public static readonly ID AIInstructions = new ID("{C3FF9061-BA31-440C-BF42-5D86BA49428D}");
                public static readonly ID Temperature = new ID("{0711B2F1-6008-4A6F-997A-D21C5F2A91D3}");
                public static readonly ID MaxTokens = new ID("{95AD4DC8-60BA-4CDA-86DA-49124CCE8CB7}");
                public static readonly ID UseInputPromptForMaxTokens = new ID("{B859D4B5-D112-4BF1-BFA6-A264F723694D}");
                public static readonly ID TopP = new ID("{D01BCA9F-D507-4886-8B04-D3E885C6715C}");
                public static readonly ID FrequencyPenalty = new ID("{C2760802-8A61-4FDB-B3A5-E082102FC585}");
                public static readonly ID PresencePenalty = new ID("{3155E60B-9CBD-4996-8F34-FB9CFA810454}");
                public static readonly ID NumberOfChoices = new ID("{56CF7E27-0BF8-42D3-A8C2-4B052F66FB6F}");
                public static readonly ID Model = new ID("{98C8CC87-811F-4D54-96D4-9E79C4349907}");
                public static readonly ID APIConfiguration = new ID("{4ACEA545-E837-4EBF-B7D9-ECBD73E083F1}");
            }
        }

        public struct APIConfiguration 
        {
            public static readonly ID ID = new ID("{8D666258-D634-4574-B9B1-A9346348CDFE}");

            public struct Fields
            {
                public static readonly ID APIKey = new ID("{EB009741-CA71-4094-886D-16F9D74CB1AC}");
                public static readonly ID APIEndpoint = new ID("{86959CFA-F9BC-4745-A177-A5A8544E09B5}");
            }
            
        }
  }
}