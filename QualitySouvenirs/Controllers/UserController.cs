using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using System.Web;

namespace QualitySouvenirs.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0 )
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public IActionResult AddOrEdit(User userModel)
        {
            if(_context.Users.Any(x =>x.Name == userModel.Name))
            {
                ViewBag.DuplicateMessage = "Username already exist.";
                return View("AddOrEdit", new User());
            }
            _context.Add(userModel);
            _context.SaveChanges();
            ModelState.Clear();
            ViewBag.SuccessMessage="Registration Successful.";
            return View("AddOrEdit", new User());


        }
    }
}