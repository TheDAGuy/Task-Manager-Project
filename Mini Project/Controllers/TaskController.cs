using Microsoft.AspNetCore.Mvc;
using Mini_Project.Data;
using Mini_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;


namespace Mini_Project.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TaskInfo> taskList = _context.Tasks.Include(a => a.AssignedEmployee).ToList();
            return View(taskList);
        }
        public IActionResult Create()
        {
            List<Employee> employed = _context.Employees.ToList();
            ViewBag.Employees = new SelectList(employed, "EmployeeId", "Name"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskInfo obj)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);  
        }

        public IActionResult Delete(int? id)
        {
            if (id  == null || id == 0)
            {
                return NotFound();
            }
            TaskInfo task = _context.Tasks.Find(id);
            
            return View(task);
          
            
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePostaction(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TaskInfo task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            TempData["success"] = "Task deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Reassign(int? id)
        {
            List<Employee> employed = _context.Employees.ToList();
            ViewBag.Employees = new SelectList(employed, "EmployeeId", "Name");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskInfo task = _context.Tasks.Find(id);

            return View(task);

        }
        [HttpPost]
        public IActionResult Reassign(int id, int EmployeeId)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            task.EmployeeId = EmployeeId;

            _context.Tasks.Update(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
