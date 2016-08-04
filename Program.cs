using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace aspnetcoreapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var testEmployees = new List<Employee>();
            testEmployees.Add(new Employee(1, "Price", "Ken"));
            testEmployees.Add(new Employee(2, "Price", "Jason"));
            testEmployees.Add(new Employee(3, "Teskey", "Samantha"));
            testEmployees.Add(new Employee(4, "Thomas", "Justin"));
            testEmployees.Add(new Employee(5, "Average", "Joe"));
            testEmployees.Add(new Employee(6, "Tim", "Chad"));
            var itDeparment = new Department(1, "IT and Fire Safety");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
