namespace EmployeeApi.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public decimal PayPerHour { get; set; }
        public Person? Person { get; set; }
    }

}
