// -------- DEPENDENCIES --------
using Sitecore.AspNet.RenderingEngine.Extensions; // <IServiceCollection>.AddSitecoreRenderingEngine()
using Sitecore.LayoutService.Client.Extensions; // <IServiceCollection>.AddSitecoreLayoutService()
using Sitecore.LayoutService.Client.Newtonsoft.Extensions; // .AddNewtonsoftJson() (which is <JsonSerializerSettings>.SetDefaults...)
using Sitecore.LayoutService.Client.Request; // <SitecoreLayoutRequest>.SiteName

// -------- BUILDER --------
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers() // For "SayHello"
    ;
builder.Services
    .AddRouting()
    .AddMvc()
    .AddNewtonsoftJson(o => o.SerializerSettings.SetDefaults()) // Because Sitecore says so
    ;
builder.Services
    .AddSitecoreLayoutService() // Tell an <ISitecoreLayoutClient> where to look for data
    .WithDefaultRequestOptions(request => { request.SiteName("examplesitename").ApiKey("{00000000-0000-0000-0000-000000000000}"); })
    .AddHttpHandler("default", "https://YOURMOCKSERVERDOMAIN/sitecore/api/layout/render/jss")
    .AsDefaultHandler()
    ;
builder.Services
    .AddSitecoreRenderingEngine() // Use of "Views" complains without this
    ;

// -------- APP ROUTING --------
var app = builder.Build();
app.MapControllers(); // For "SayHello"
app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) => string.Join("\n", endpointSources.SelectMany(source => source.Endpoints))); // curl http://localhost:5000/debug/routes debugging

app.UseRouting(); // .UseEndpoints() runtime-fails if this has not yet been called.
#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    // Fall back to language-less routing as well, and use the default culture (en).
    endpoints.MapFallbackToController("Index", "Default");
});
#pragma warning restore ASP0014

// -------- APP RUN --------
app.Run();