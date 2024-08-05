using Sitecore.AspNet.RenderingEngine.Binding.Attributes; // [SitecoreRouteField] and [SitecoreRouteFieldAttribute] types
using Sitecore.LayoutService.Client.Response.Model.Fields; // TextField type

namespace Handwritten.Models
{
    public class XyzzyModel
    {
        [SitecoreRouteField]
        public TextField title { get; set; }

        [SitecoreRouteField]
        public TextField text { get; set; }

        public string CopyrightYear => DateTime.Now.ToString("yyyy");
    }
}