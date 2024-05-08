using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerMis.Data;
using SerMis.Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SerMis.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = null;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
                return View();

            var userInfo = await _context.UserInfo.FirstOrDefaultAsync(u => u.Name == name);

            if (userInfo == null)
            {
                userInfo = new UserInfo { Name = name };
                _context.UserInfo.Add(userInfo);
                await _context.SaveChangesAsync();
            }
            ViewBag.UserId = userInfo.Id;
            ViewBag.UserName = userInfo.Name;
            ViewBag.City = userInfo.City;
            ViewBag.Age = userInfo.Age;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(int userId, string field, string value)
        {
            var userInfo = await _context.UserInfo.FindAsync(userId);

            if (userInfo == null)
                return NotFound();

            if (field.Equals("city"))
                userInfo.City = value;
            if (field.Equals("age"))
                userInfo.Age = int.Parse(value);

            _context.UserInfo.Update(userInfo);
            await _context.SaveChangesAsync();

            return Json("Success");
        }
    }
}
