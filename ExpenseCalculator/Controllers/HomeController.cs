using ExpenseCalculator.Data;
using ExpenseCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpensesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id.HasValue)
            {
                var expense = _context.Expenses.Find(id);
                if (expense != null)
                {
                    return View(expense);
                }
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {

            var existingExpense = _context.Expenses.Find(model.ID);
            if (existingExpense != null)
            {
                existingExpense.Value = model.Value;
                existingExpense.Description = model.Description;
            }
            else
            {
                _context.Expenses.Add(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
