using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Pipelines;
using Sitecore.Pipelines.FixXHtml;
using Sitecore.Shell.Applications.ContentManager.ReturnFieldEditorValues;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using SitecoreRedemption.Foundation.AIServices.Models;
using SitecoreRedemption.Foundation.AIServices.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.HtmlControls;

namespace SitecoreRedemption.Feature.AIContentOptimizer.Dialogs
{
    public class OptimizeAI : XamlMainControl
    {
        //temporary hack until a data driven XAML control can be swapped in.
        protected Scrollbox Fixed;
        protected Scrollbox Fixed2;
        protected Scrollbox Fixed3;
        protected Scrollbox Fixed4;

        protected Literal Seo;
        protected Literal Seo2;
        protected Literal Seo3;
        protected Literal Seo4;

        protected Literal Readability;
        protected Literal Readability2;
        protected Literal Readability3;
        protected Literal Readability4;

        protected Literal Accessibility;
        protected Literal Accessibility2;
        protected Literal Accessibility3;
        protected Literal Accessibility4;

        protected HtmlGenericControl Response;
        protected HtmlGenericControl Response2;
        protected HtmlGenericControl Response3;
        protected HtmlGenericControl Response4;

        protected Radiobutton Choice1;
        protected Radiobutton Choice2;
        protected Radiobutton Choice3;
        protected Radiobutton Choice4;
        /// <summary></summary>
        protected Scrollbox Original;
        /// <summary></summary>
        protected Border ScrollBorder;
        /// <summary></summary>
        protected Tabstrip TabStrip;

        protected List<Choice> Choices;

        protected string SelectedChoice = string.Empty;

        /// <summary>Gets the fixed HTML.</summary>
        /// <value>The fixed HTML.</value>
        public string FixedHtml
        {
            get => StringUtil.GetString(this.ViewState[nameof(FixedHtml)]);
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(FixedHtml)] = (object)value;
            }
        }
        public string FixedHtml2
        {
            get => StringUtil.GetString(this.ViewState[nameof(FixedHtml2)]);
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(FixedHtml2)] = (object)value;
            }
        }
        public string FixedHtml3
        {
            get => StringUtil.GetString(this.ViewState[nameof(FixedHtml3)]);
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(FixedHtml3)] = (object)value;
            }
        }
        public string FixedHtml4
        {
            get => StringUtil.GetString(this.ViewState[nameof(FixedHtml4)]);
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(FixedHtml4)] = (object)value;
            }
        }

        /// <summary>Gets or sets the original HTML.</summary>
        /// <value>The original HTML.</value>
        public string OriginalHtml
        {
            get => StringUtil.GetString(this.ViewState[nameof(OriginalHtml)]);
            set
            {
                Assert.ArgumentNotNull((object)value, nameof(value));
                this.ViewState[nameof(OriginalHtml)] = (object)value;
            }
        }

        private IContentOptimizerService _contentOptimizerService;

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            HasAccess();
            base.OnLoad(e);
            if (AjaxScriptManager.Current.IsEvent)
            {                
                return;
            }
            string str = HttpUtility.HtmlDecode(this.SanitizeHtml(StringUtil.GetString(UrlHandle.Get()["html"])));
            string setting = HttpUtility.HtmlDecode(StringUtil.GetString(UrlHandle.Get()["setting"]));
            this.OriginalHtml = str;
            try
            {
                this.Original.InnerHtml = RuntimeHtml.Convert(str, Settings.HtmlEditor.SupportWebControls);
            }
            catch
            {
            }
            //this.OriginalMemo.Value = str;

            this._contentOptimizerService = ServiceLocator.ServiceProvider.GetService<IContentOptimizerService>();

            //var aiTask = _contentOptimizerService.OptimizeContentAsync(str);

            Task<OpenAiResponse> task = Task.Run(() => _contentOptimizerService.OptimizeContentAsync(str, setting));
            task.Wait();

            var aiResult = task.Result;

            foreach (var choice in aiResult.Choices)
            {
                if (choice.Message?.Content?.Content == null)
                    continue;

                var choiceStr = choice.Message.Content.Content;

                FixXHtmlArgs args = new FixXHtmlArgs(choiceStr);
                using (new LongRunningOperationWatcher(Settings.Profiling.RenderFieldThreshold, "fixXHtml", Array.Empty<string>()))
                    CorePipeline.Run("fixXHtml", (PipelineArgs)args);
                choice.Message.Content.FixedHtml = args.Html;
            }

            SetValues(aiResult.Choices);



        }

      

        private void SetValues(List<Choice> choices)
        {
            //Temporary hack as no suitable XAML Control was found that accepts a data binding and allows for custom HTML template.
            if (choices.Count >= 1)
            {
                var choice = choices.FirstOrDefault();
                string html = StringUtil.GetString(choice?.Message.Content.FixedHtml);
                this.FixedHtml = html;
                this.Fixed.InnerHtml = html;
                this.Seo.Text = choice.Message.Content.SeoScore.ToString();
                this.Readability.Text = choice.Message.Content.Readability.ToString();
                this.Accessibility.Text = choice.Message.Content.Accessibility.ToString();
                this.Response.Visible = true;

            }
            if (choices.Count >= 2)
            {
                var choice = choices.FirstOrDefault(c => c.Index == 1);
                string html = StringUtil.GetString(choice?.Message.Content.FixedHtml);
                this.FixedHtml2 = html;
                this.Fixed2.InnerHtml = html;
                this.Seo2.Text = choice.Message.Content.SeoScore.ToString();
                this.Readability2.Text = choice.Message.Content.Readability.ToString();
                this.Accessibility2.Text = choice.Message.Content.Accessibility.ToString();
                this.Response2.Visible = true;
            }
            if (choices.Count >= 3)
            {
                var choice = choices.FirstOrDefault(c => c.Index == 2);
                string html = StringUtil.GetString(choice?.Message.Content.FixedHtml);
                this.FixedHtml3 = html;
                this.Fixed3.InnerHtml = html;
                this.Seo3.Text = choice.Message.Content.SeoScore.ToString();
                this.Readability3.Text = choice.Message.Content.Readability.ToString();
                this.Accessibility3.Text = choice.Message.Content.Accessibility.ToString();
                this.Response3.Visible = true;
            }
            if (choices.Count >= 4)
            {
                var choice = choices.FirstOrDefault(c => c.Index == 3);
                string html = StringUtil.GetString(choice?.Message.Content.FixedHtml);
                this.FixedHtml4 = html;
                this.Fixed4.InnerHtml = html;
                this.Seo4.Text = choice.Message.Content.SeoScore.ToString();
                this.Readability4.Text = choice.Message.Content.Readability.ToString();
                this.Accessibility4.Text = choice.Message.Content.Accessibility.ToString();
                this.Response4.Visible = true;
            }
        }

        private static void HasAccess()
        {
            Item obj1 = Sitecore.Client.CoreDatabase.GetItem("/sitecore/content/Applications/Content Editor/Dialogs/EditHtml/Ribbon/Home/Write/Fix");
            Item obj2 = Sitecore.Client.CoreDatabase.GetItem("/sitecore/system/Field types/Simple Types/Rich Text/Menu/Suggest Fix");
            Assert.HasAccess(obj1 != null && obj2 != null && (obj1.Access.CanRead() || obj2.Access.CanRead()), "Access denied");
        }

        /// <summary>Prepares the HTML for control.</summary>
        /// <param name="originalHtml">The original HTML.</param>
        /// <returns>Prepared html for web control. Sanitized from XSS.</returns>
        protected internal string SanitizeHtml(string originalHtml)
        {
            string preparedHtml = originalHtml;

            return this.EncodeHtml(preparedHtml);
        }

        /// <summary>Counts the errors.</summary>
        /// <param name="html">The HTML.</param>
        /// <param name="count">The count.</param>
        private static void CountErrors(string html, Literal count)
        {
            Assert.ArgumentNotNull((object)html, nameof(html));
            Assert.ArgumentNotNull((object)count, nameof(count));
            html = string.Format("<div>{0}</div>", (object)html);
            html = XHtml.MakeDocument(html, true);
            Collection<XHtmlValidatorError> collection = new XHtmlValidator(html).Validate();
            count.Text = Translate.Text(collection.Count == 1 ? "{0} error" : "{0} errors", (object)collection.Count);
        }

        /// <summary>Views the errors.</summary>
        /// <param name="html">The HTML.</param>
        private static void ViewErrors(string html)
        {
            Assert.ArgumentNotNull((object)html, nameof(html));
            UrlString urlString = new UrlString("/sitecore/shell/-/xaml/Sitecore.Shell.Applications.ContentEditor.Dialogs.EditHtml.ValidateXHtml.aspx");
            new UrlHandle() { [nameof(html)] = html }.Add(urlString);
            SheerResponse.ShowModalDialog(urlString.ToString());
        }

        /// <summary>Encodes the HTML.</summary>
        /// <param name="preparedHtml">The prepared HTML.</param>
        /// <returns>Returns encoded HTML.</returns>
        protected internal virtual string EncodeHtml(string preparedHtml)
        {
            return HttpUtility.HtmlEncode(preparedHtml);
        }

        /// <summary>Handles a click on the OK button.</summary>
        /// <remarks>When the user clicks OK, the dialog is closed by calling
        /// the <see cref="M:Sitecore.Web.UI.Sheer.ClientResponse.CloseWindow">CloseWindow</see> method.</remarks>
        [ProcessorMethod]
        protected virtual void OK_Click()
        {
            //var choice1Checked = (WebUtil.GetFormValue("Choice1") == "1");
            //var choice2Checked = (WebUtil.GetFormValue("Choice2") == "1");
            //var choice3Checked = (WebUtil.GetFormValue("Choice3") == "1");
            //var choice4Checked = (WebUtil.GetFormValue("Choice4") == "1");
            string str = string.Empty;

            //if (choice1Checked)
            //    str = this.FixedHtml;
            //if (choice2Checked)
            //    str = this.FixedHtml2;
            //if (choice3Checked)
            //    str = this.FixedHtml3;
            //if (choice4Checked)
            //    str = this.FixedHtml4; 

            //if (SelectedChoice == "Choice1")
            //    str = this.FixedHtml;
            //if (SelectedChoice == "Choice2")
            //    str = this.FixedHtml2;
            //if (SelectedChoice == "Choice3")
            //    str = this.FixedHtml3;
            //if (SelectedChoice == "Choice4")
            //    str = this.FixedHtml4;

            //if (string.IsNullOrEmpty(str))
            //    str = "__#!$No value$!#__";

            //There is a bug with the viewstate
            //setting this to specific choice for now

            str = this.FixedHtml;

            if (!string.IsNullOrEmpty(str))
                SheerResponse.SetDialogValue(str);

            SheerResponse.CloseWindow();
        }

        /// <summary>Handles the Cancel click event.</summary>
        [ProcessorMethod]
        protected virtual void Cancel_Click() => SheerResponse.CloseWindow();
    }
}
