using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace aspnetcoreapp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            int idParam;
            string output = null;
            const string greeting = "Hello there! You can request an employee by navigating to /employees?id=1 or a department with /departments?id=1";
            app.Run(context =>
            {
                if (!context.Request.Path.HasValue) {
                    return context.Response.WriteAsync(greeting);
                }
                switch (context.Request.Path) {
                    case "/employees":
                        if (!int.TryParse(context.Request.Query["id"], out idParam)) {
                            return context.Response.WriteAsync("Please provide a valid id for employee!");
                        }
                        output = Program.getSerializedEmployee(idParam);
                        if (output == null) {
                            return context.Response.WriteAsync("Not found");
                        } else {
                            return context.Response.WriteAsync(output);
                        }
                    case "/departments":
                        if (!int.TryParse(context.Request.Query["id"], out idParam)) {
                            return context.Response.WriteAsync("Please provide a valid id for department!");
                        }
                        output = Program.getSerializedDepartment(idParam);
                        if (output == null) {
                            return context.Response.WriteAsync("Not found");
                        } else {
                            return context.Response.WriteAsync(output);
                        }
                }
                return context.Response.WriteAsync(greeting);
            });
        }
    }
}
