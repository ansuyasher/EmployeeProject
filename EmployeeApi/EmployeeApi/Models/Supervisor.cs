namespace EmployeeApi.Models
{
    public class Supervisor
    {
        public int SupervisorID { get; set; }
        public int PersonID { get; set; }
        public decimal AnnualSalary { get; set; }
        public Person? Person { get; set; }
    }

}
