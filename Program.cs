using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace aspnetcoreapp
{
    public class Program
    {
        public static Dictionary<int, Employee> testEmployees {get;  set;}
        public static Dictionary<int, Department> testDepartments {get; set;}
        public static string getSerializedEmployee(int id) {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));

            using(StringWriter writer = new StringWriter()) {
                Employee employee;
                if (testEmployees.TryGetValue(id, out employee)) {
                    serializer.Serialize(writer, testEmployees[id]);
                    return writer.ToString();
                } else {
                    return null;
                }
            }
        }
        public static string getSerializedDepartment(int id) {
            XmlSerializer serializer = new XmlSerializer(typeof(Department));

            using(StringWriter writer = new StringWriter()) {
                Department dept;
                if (testDepartments.TryGetValue(id, out dept)) {
                    serializer.Serialize(writer, testDepartments[id]);
                    return writer.ToString();
                } else {
                    return null;
                }
            }
        }
        private static void populateTestData() {
            // Please excuse the test data.
            var employees = new List<Employee>();
            var departments = new List<Department>();
            employees.Add(new Employee(1, "Price", "Ken"));
            employees.Add(new Employee(2, "Price", "Jason"));
            employees.Add(new Employee(3, "Teskey", "Samantha"));
            employees.Add(new Employee(4, "Thomas", "Justin"));
            employees.Add(new Employee(5, "Average", "Joe"));
            employees.Add(new Employee(6, "Tim", "Chad"));
            departments.Add(new Department(1, "IT and Fire Safety"));
            departments.Add(new Department(2, "Marketing"));
            departments[0].addEmployee(employees[0]);
            departments[0].addEmployee(employees[2]);
            departments[1].addEmployee(employees[1]);
            departments[1].addEmployee(employees[3]);
            departments[1].addEmployee(employees[4]);
            departments[1].addEmployee(employees[5]);
            testEmployees = new Dictionary<int, Employee>();
            testDepartments = new Dictionary<int, Department>();
            employees.ForEach(delegate(Employee emp) {
                testEmployees.Add(emp.id, emp);
            });
            departments.ForEach(delegate(Department dep) {
                testDepartments.Add(dep.id, dep);
            });
        }
        public static void Main(string[] args)
        {
            populateTestData();
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
