using Yarp.ReverseProxy;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Configure(builder.Configuration.GetSection("Kestrel"));
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpMessageHandlerBuilder((context, proxyBuilder) =>
    {
        var cluster = context.Cluster;
        if (cluster?.Metadata != null &&
            cluster.Metadata.TryGetValue("UseProxy", out var useProxy) &&
            useProxy?.ToLower() == "true")
        {
            proxyBuilder.PrimaryHandler = new HttpClientHandler
            {
                Proxy = new WebProxy("http://your-proxy-host:your-proxy-port"),
                UseProxy = true
            };
        }
        else
        {
            proxyBuilder.PrimaryHandler = new HttpClientHandler
            {
                UseProxy = false
            };
        }
    });

var app = builder.Build();
app.MapReverseProxy();
app.Run();
