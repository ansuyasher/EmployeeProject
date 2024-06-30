namespace EmployeeApi.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public int PersonID { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }
        public Person? Person { get; set; }
    }

}
