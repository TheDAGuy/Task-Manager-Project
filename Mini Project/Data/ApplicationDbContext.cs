using Microsoft.EntityFrameworkCore;
using Mini_Project.Models;

namespace Mini_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TaskInfo> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Comments> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskInfo>()
                .HasOne(t => t.AssignedEmployee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Email = "JohnDoe1824@gmail.com" , Level = EmployeeLevel.Employee},
                new Employee { EmployeeId = 2, FirstName = "Levy", LastName = "Ackerman", Email = "TitanSlayer@gmail.com", Level = EmployeeLevel.Employee },
                new Employee { EmployeeId = 3, FirstName = "Eren", LastName = "Yaeager", Email = "MysticBoy@gmail.com" , Level = EmployeeLevel.Employee },
                new Employee { EmployeeId = 4, FirstName = "John", LastName = "Wick", Email = "Puppylover@gmail.com", Level = EmployeeLevel.Employee },
                new Employee { EmployeeId = 5, FirstName = "Pe", LastName = "Doe", Email = "Drake4eva@gmail.com" , Level = EmployeeLevel.Admin }
            );

            modelBuilder.Entity<TaskInfo>().HasData(
                new TaskInfo { TaskId = 1, Name = "Make a giraffe", Description = "Make a 3D printed Model of a Giraffe", DueDate = new DateOnly(2024, 8, 16), Status = Status.New, EmployeeId = 1 },
                new TaskInfo { TaskId = 2, Name = "Make a dolphin", Description = "Make a 3D printed Model of a Dolphin", DueDate = new DateOnly(2024, 7, 9), Status = Status.New, EmployeeId = 2 },
                new TaskInfo { TaskId = 3, Name = "Make a lion", Description = "Make a 3D printed Model of a Lion", DueDate = new DateOnly(2024, 8, 6), Status = Status.New, EmployeeId = 3 },
                new TaskInfo { TaskId = 4, Name = "Make a car", Description = "Make a 3D printed Model of a Car", DueDate = new DateOnly(2024, 10, 26), Status = Status.New, EmployeeId = 4 },
                new TaskInfo { TaskId = 5, Name = "Make a house", Description = "Make a 3D printed Model of a House", DueDate = new DateOnly(2024, 9, 24), Status = Status.New, EmployeeId = 5 }
            );

            modelBuilder.Entity<Comments>().HasData(
                new Comments { CommentId = 1, Content = "Started with the task", Timestamp = DateTime.Now, EmployeeId = 1, TaskId = 1 },
                new Comments { CommentId = 2, Content = "In progress", Timestamp = DateTime.Now, EmployeeId = 1, TaskId = 1 },  
                new Comments { CommentId = 3, Content = "Completed", Timestamp = DateTime.Now, EmployeeId = 3, TaskId = 3 },
                new Comments { CommentId = 4, Content = "Reviewed", Timestamp = DateTime.Now, EmployeeId = 4, TaskId = 4 },
                new Comments { CommentId = 5, Content = "Started with the task", Timestamp = DateTime.Now, EmployeeId = 5, TaskId = 5 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

