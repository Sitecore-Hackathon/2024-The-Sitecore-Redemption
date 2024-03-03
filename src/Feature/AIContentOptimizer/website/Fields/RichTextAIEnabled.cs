using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreRedemption.Feature.AIContentOptimizer.Fields
{
    public class RichTextAIEnabled : RichText
    {
        private string _setting { get; set; }

        /// <summary>Handles the message.</summary>
        /// <param name="message">The message.</param>
        public override void HandleMessage(Message message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            if (!ShouldHandleMessage(message))
            {
                return;
            }

            if (IsAIOptimizeClick(message))
            {
                if (!string.IsNullOrEmpty(message["setting"]))
                {
                    _setting = message["setting"];
                }
                Sitecore.Context.ClientPage.Start((object)this, "Optimize");
                return;
            }
                       
            base.HandleMessage(message);
        }

        private bool IsCurrentControl(Message message)
        {
            return AreEqualIgnoreCase(message["id"], ID);
        }

        private static bool IsAIOptimizeClick(Message message)
        {
            return AreEqualIgnoreCase(message.Name, "airichtext:optimize");
        }

        private bool ShouldHandleMessage(Message message)
        {
            return IsCurrentControl(message)
                    && !string.IsNullOrWhiteSpace(message.Name);
        }

        private static bool AreEqualIgnoreCase(string one, string two)
        {
            return string.Equals(one, two, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>Utilize AI to optimize the text</summary>
        /// <param name="args">The args.</param>
        protected void Optimize(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            if (Disabled)
                return;
            if (args.IsPostBack)
            {
                if (args.Result == null || !(args.Result != "undefined"))
                    return;
                UpdateHtml(args);
            }
            else
            {
                UrlString urlString = new UrlString("/sitecore/shell/-/xaml/SitecoreRedemption.Feature.AIContentOptimizer.Dialogs.OptimizeAI.aspx");
                UrlHandle urlHandle = new UrlHandle();
                string empty = Value;
                if (empty == "__#!$No value$!#__")
                    empty = string.Empty;
                urlHandle["html"] = empty;
                urlHandle["setting"] = this._setting;
                urlHandle.Add(urlString);
                
                SheerResponse.ShowModalDialog(urlString.ToString(), "800px", "700px", string.Empty, true);
                args.WaitForPostBack();
            }
        }
    }
}
