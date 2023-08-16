using Microsoft.AspNetCore.Mvc;
using EmployeeSystem.Models;
using EmployeeSystem.Data;
using System.Linq;

namespace EmployeeSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeSystem.Models.Employee _dbContext;

        public EmployeeController(EmployeeSystem.Data.Employee dbContext)
        {
            _dbContext = dbContext;
        }

        // Index action - Read (List) operation
        public IActionResult Index()
        {
            var employees = _dbContext.Employees.ToList();
            return View(employees);
        }

        // Create action
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeSystem.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Edit action
        public IActionResult Edit(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeSystem.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Update(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Delete action
        public IActionResult Delete(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges(); // Call SaveChanges on the context, not on the Employee instance
            }
            return RedirectToAction("Index");
        }

    }
}
