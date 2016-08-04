using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace aspnetcoreapp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                Console.Out.WriteLine(context.Request.Path);
                if (!context.Request.Path.HasValue) {
                    return context.Response.WriteAsync("Hello there! You can request an employee by navigating to /employees?id=1 or a department with /departments?id=1");
                }
                switch (context.Request.Path) {
                    case "/employees":
                        Console.Out.WriteLine(context.Request.Query["id"]);
                        break;
                    case "/departments":
                        Console.Out.WriteLine(context.Request.Query["id"]);
                        break;
                }
                return context.Response.WriteAsync("Hello from ASP.NET Core!");
            });
        }
    }
}
