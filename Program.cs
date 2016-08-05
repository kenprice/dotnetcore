using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;

namespace aspnetcoreapp
{
    public class Program
    {
        private static Dictionary<int, Employee> employeesDict = new Dictionary<int, Employee>();
        private static Dictionary<int, Department> departmentsDict = new Dictionary<int, Department>();
        private static List<Employee> employees = new List<Employee>();
        private static List<Department> departments = new List<Department>();

        public static string getSerializedEmployee(int id) {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));

            Employee employee;
            if (employeesDict.TryGetValue(id, out employee)) {
                using(StringWriter writer = new StringWriter()) {
                    serializer.Serialize(writer, employeesDict[id]);
                    return writer.ToString();
                }
            } else {
                return null;
            }
        }

        public static string getSerializedDepartment(int id) {
            XmlSerializer serializer = new XmlSerializer(typeof(Department));

            Department dept;
            if (departmentsDict.TryGetValue(id, out dept)) {
                using(StringWriter writer = new StringWriter()) {
                    serializer.Serialize(writer, departmentsDict[id]);
                    return writer.ToString();
                }
            } else {
                return null;
            }
        }

        private static void populateTestData() {
            // Please excuse the test data.
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

            populateDict();
        }

        private static void populateDict() {
            employees.ForEach(delegate(Employee emp) {
                employeesDict.Add(emp.id, emp);
            });
            departments.ForEach(delegate(Department dep) {
                departmentsDict.Add(dep.id, dep);
            });
        }

        private static void saveSerializedData() {
            XmlSerializer empSerializer = new XmlSerializer(typeof(List<Employee>));
            XmlSerializer depSerializer = new XmlSerializer(typeof(List<Department>));
            using(Stream writer = File.Open("employees.xml", FileMode.Create)) {
                empSerializer.Serialize(writer, employees);
            }
            using(Stream writer = File.Open("departments.xml", FileMode.Create)) {
                depSerializer.Serialize(writer, departments);
            }
        }

        private static void loadSerializedData() {
            XmlSerializer empSerializer = new XmlSerializer(typeof(List<Employee>));
            XmlSerializer depSerializer = new XmlSerializer(typeof(List<Department>));
            using(StreamReader reader = new StreamReader(File.OpenRead("employees.xml"))) {
                employees = (List<Employee>)empSerializer.Deserialize(reader);
            }
            using(StreamReader reader = new StreamReader(File.OpenRead("departments.xml"))) {
                departments = (List<Department>)depSerializer.Deserialize(reader);
            }
            populateDict();

            // Employee objects in each Department object should refer to the same objects
            // as those in the employee list. So let's restore these relationships.
            departments.ForEach(delegate(Department dep) {
                for (int i = 0; i < dep.employees.Count; i++) {
                    if (employeesDict.ContainsKey(dep.employees[i].id)) {
                        dep.employees[i] = employeesDict[dep.employees[i].id];
                    }
                }
            });
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0) {
                // Normal mode - fetch employee/department data from xml files
                loadSerializedData();
            } else if (args.Length == 1 && args[0] == "test") {
                populateTestData();
                saveSerializedData();
            } else {
                Console.WriteLine("Usage: fakeco-server [test]\n'test' will create new employees and departments.");
                return;
            }

            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
