using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    // vid part 9 at 45 min about Authorize to redirect users to login

    [Authorize]
    public class ShoppingItemsController : Controller
    {
        // video part 9 around 13.20 point

        private readonly ApplicationDbContext _context;
        // we get this built in class from identity -In any of your controllers 
        // that need to access the currently authenticated user inject aka add this UserManager
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager )
        {
             _context = context;
            _userManager = userManager;
        }
        
        
        // GET: ShoppingItems

           
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            // part 9 vid explaination of how to filter for users items
            var items = await _context.ShoppingItem
                .Where(si => si.ApplicationUserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        // GET: ShoppingItems/Details/5

        // part 9 vid at 39 min
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingItems/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ShoppingItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShoppingItem shoppingItem)
        {
            try
            {
                
                // vid part 9 at 23.40 min pt 
                var user = await GetCurrentUserAsync();
                shoppingItem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Add(shoppingItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.ShoppingItem.FirstOrDefaultAsync(si => si.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (item.ApplicationUserId != loggedInUser.Id)

            {
                return NotFound();
            }
              
                   return View();
        }

        // POST: ShoppingItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ShoppingItem shoppingItem)
        {
            try
            {
                // part 9 vid at 1.15 pt explains why we get the currentUser again for security
                // we trust the cookie but we dont trust the form field values

                var user = await GetCurrentUserAsync();
                shoppingItem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Update(shoppingItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItem
                //.Include(si => si.ProductName)
                .FirstOrDefaultAsync(si => si.Id == id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return View(shoppingItem);
        }

        //// GET: TodoItems/Delete/5
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var shoppingItem = await _context.ShoppingItem.Include(si => si.ProductName).FirstOrDefaultAsync(si => si.Id == id);
        //            var todoItem = await _context.TodoItem.Include(ti => ti.TodoStatus).FirstOrDefaultAsync(ti => ti.Id == id);
        //    return View(shoppingItem);
        //}

        //// GET: ShoppingItems/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View(shoppingItem);
        //}


        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);
            _context.ShoppingItem.Remove(shoppingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Delete(int id, ShoppingItem shoppingItem)
        //{
        //    try
        //    {
        //        _context.ShoppingItem.Remove(shoppingItem);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// POST: ShoppingItems/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
       
        // vid part 9 at 22.40 min pt
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}