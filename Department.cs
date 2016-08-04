using System.Collections.Generic;

namespace aspnetcoreapp
{
    public class Department
    {
        private int id;
        public string name {get; set;}
        private List<Employee> employees = new List<Employee>();

        public List<Employee> getEmployees() {
            return this.employees;
        }

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
    }
}
