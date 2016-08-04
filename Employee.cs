namespace aspnetcoreapp
{
    public class Employee
    {
        public int id {get; set;}
        public string lastname {get; set;}
        public string firstname {get; set;}

        public Employee(int id, string lastname, string firstname) {
            this.id = id;
            this.lastname = lastname;
            this.firstname = firstname;
        }
    }
}
