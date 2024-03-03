using SitecoreRedemption.Foundation.ORM.Models;

namespace SitecoreRedemption.Foundation.Content.Tests.Models
{
    public interface ITestItem : IGlassBase
    {
        string Title { get; set; }
    }
}
