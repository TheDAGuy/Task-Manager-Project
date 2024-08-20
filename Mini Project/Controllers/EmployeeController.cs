using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Project.Data;
using Mini_Project.Models;
using System.Threading.Tasks;

namespace Mini_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _context.Employees.Include(e => e.Tasks).ToList();
            return View(employees);
        }

        public async Task<IActionResult> Details(int employeeId, int taskId)
        {
            var employee = _context.Employees
                .Include(e => e.Tasks)
                .ThenInclude(t => t.Comments)
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var task = employee.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                return NotFound();
            }

            if (task.Status == Status.New && task.AssignedEmployee?.EmployeeId == employeeId)
            {
                task.Status = Status.InProgress;
                _context.Update(task);
                await _context.SaveChangesAsync();
            }

            ViewBag.Task = task;
            ViewBag.Comments = task.Comments.OrderBy(c => c.Timestamp).ToList();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int taskId, Status taskStatus)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            if (taskStatus == Status.Completed)
            {
                task.Status = taskStatus;
                _context.Update(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { employeeId = task.AssignedEmployee?.EmployeeId, taskId = task.TaskId });
        }

        [HttpPost]
        public IActionResult AddComment(int employeeId, int taskId, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                return RedirectToAction("Details", new { employeeId = employeeId, taskId = taskId });
            }

            TaskInfo task = _context.Tasks
                .Include(t => t.AssignedEmployee)
                .FirstOrDefault(t => t.TaskId == taskId && t.AssignedEmployee.EmployeeId == employeeId);

            if (task == null)
            {
                return NotFound();
            }

            Comments comments = new Comments
            {
                Content = commentText,
                Timestamp = DateTime.Now,
                EmployeeId = employeeId,
                TaskId = taskId
            };

            _context.Comments.Add(comments);
            _context.SaveChanges();

            return RedirectToAction("Details", new { employeeId = employeeId, taskId = taskId });
        }

        public IActionResult AddEmployees()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Task");
            }
            return View(employee);
        }
    }
}
