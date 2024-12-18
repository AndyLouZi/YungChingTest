using Infrasturcture;
using InterFace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace YungChingTest.Controllers
{
    public class CustomersController:Controller
    {
        private readonly ApplicationDbContext _context;
        private ICustomersCRUDService _customersCRUDService;

        public CustomersController(ApplicationDbContext context, ICustomersCRUDService customersCRUDService)
        {
            _context = context;
            _customersCRUDService = customersCRUDService;
        }

        // 顯示所有產品
        public IActionResult Index()
        {
            var products = _context.Customers.ToList();
            return View(products);
        }

        // 顯示創建產品頁面
        public IActionResult Create()
        {
            return View();
        }

        // 處理創建產品
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Customers customers)
        {

                bool success = _customersCRUDService.CreateCustomer(customers);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 失敗的處理邏輯
                    ModelState.AddModelError("", "There was an issue saving the customer.");
                }
            
            return View(customers);
        }

        // 查詢客戶資料
        [HttpGet]
        public IActionResult Search(string? customerID)
        {
            if (string.IsNullOrEmpty(customerID))
            {
                ViewBag.SearchPerformed = true;
                return View("Update");
            }

            var customer = _customersCRUDService.SearchCustomer(customerID);

            if (customer == null)
            {
                ViewBag.SearchPerformed = true;
                ViewBag.Customer = null;
            }
            else
            {
                ViewBag.SearchPerformed = true;
                ViewBag.Customer = customer;
            }

            return View("Update");
        }

        // 顯示更新頁面
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        // 更新客戶資料
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Customers customers)
        {

                try
                {
                    _context.Update(customers);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Customers.Any(c => c.CustomerID == customers.CustomerID))
                    {
                        return NotFound();
                    }
                    throw;
                }
            
            return View(customers);
        }

        public IActionResult Delete()
        {
            return View();
        }


        public IActionResult SearchForDelete(string customerID)
        {
            ViewBag.SearchPerformed = true;

            if (string.IsNullOrWhiteSpace(customerID))
            {
                ViewBag.Customer = null;
                return View("Delete");
            }

            var customer = _customersCRUDService.SearchCustomer(customerID);

            if (customer != null)
            {
                ViewBag.Customer = customer;
            }
            else
            {
                ViewBag.Customer = null;
            }

            return View("Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string customerID)
        {
            if (string.IsNullOrWhiteSpace(customerID))
            {
                TempData["Message"] = "Customer ID cannot be empty.";
                return RedirectToAction(nameof(SearchForDelete));
            }

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == customerID);

            if (customer == null)
            {
                TempData["Message"] = "No customer found with the provided CustomerID.";
                return RedirectToAction(nameof(SearchForDelete));
            }

            // 關閉外鍵檢查
            _context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = OFF;");
            try
            {
                // 刪除客戶
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                TempData["Message"] = "Customer deleted successfully.";
            }
            catch (Exception ex)
            {
                // 如果發生錯誤，可以捕獲並處理
                TempData["Message"] = $"Error deleting customer: {ex.Message}";
            }
            finally
            {
                // 開啟外鍵檢查
                _context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
            }

            TempData["Message"] = "Customer deleted successfully.";
            return RedirectToAction(nameof(SearchForDelete));
        }
    }
}

