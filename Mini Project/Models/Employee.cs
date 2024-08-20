using System.ComponentModel.DataAnnotations;

namespace Mini_Project.Models
{
    public enum EmployeeLevel
    {
        Admin,
        Employee
    }
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public required EmployeeLevel Level { get; set; }

        public ICollection<Comments> Comments { get; set; } = new List<Comments>();

        public ICollection<TaskInfo> Tasks { get; set; } = new List<TaskInfo>();
    }
}
