using Handwritten.Models;
using Microsoft.AspNetCore.Mvc;
using Sitecore.AspNet.RenderingEngine;
using Sitecore.AspNet.RenderingEngine.Filters;

namespace Handwritten.Controllers;

public class DefaultController : Controller
{

    [UseSitecoreRendering]
    public IActionResult Index(XyzzyModel mypage)
    {
        var request = HttpContext.GetSitecoreRenderingContext();
        return View(mypage);
    }

}