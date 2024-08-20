using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mini_Project.Models
{
    public enum Status
    {
        New,
        InProgress,
        Completed
    }
    public class TaskInfo
    {
        [Key]
        public int TaskId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Status Status { get; set; } = Status.New;

        [DisplayName("Due Date")]
        public DateOnly DueDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee? AssignedEmployee { get; set; }
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();

    }
}
