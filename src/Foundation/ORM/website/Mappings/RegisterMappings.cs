using Glass.Mapper.Sc.Pipelines.AddMaps;
using SitecoreRedemption.Foundation.ORM.Extensions;

namespace SitecoreRedemption.Foundation.ORM.Mappings
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("SitecoreRedemption.Foundation.ORM");
        }
    }
}
