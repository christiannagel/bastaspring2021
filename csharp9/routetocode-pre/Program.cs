using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

WebHost.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
    })
    .Configure((context, app) =>
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
        });
    })
    .Build().Run();