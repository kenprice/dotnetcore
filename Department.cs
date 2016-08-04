using System.Collections.Generic;

namespace aspnetcoreapp
{
    public class Department
    {
        public int id {get; set;}
        public string name {get; set;}
        public List<Employee> employees {get; set;}

        public void addEmployee(Employee employee) {
            if (!this.employees.Exists(e => e.id == employee.id)) {
                this.employees.Add(employee);
            }
        }

        public void deleteEmployee(Employee employee) {
            if (this.employees.Exists(e => e.id == employee.id)) {
                this.employees.Remove(employee);
            }
        }

        public Department(int id, string name) {
            this.id = id;
            this.name = name;
            this.employees = new List<Employee>();
        }

        public Department() {
            //Parameterless constructor required for XmlSerializer
            this.employees = new List<Employee>();
        }
    }
}
