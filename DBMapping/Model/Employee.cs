namespace DBMapping.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Department? Department { get; set; }
        public Address? Address { get; set; }
    }
}
