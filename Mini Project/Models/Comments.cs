using System.ComponentModel.DataAnnotations;

namespace Mini_Project.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public Employee? Employee { get; set; }
        public TaskInfo? Task { get; set; }
    }
}
