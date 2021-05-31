using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduPortalV2.Models;
using EduPortalV2.Models.AppDBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EduPortalV2.Controllers
{
    [Authorize()]
    public class CoursesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoursesController(AppDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var userId = UserControl();
            var user = new IdentityUser
            {
               Id=userId
            };
           
            var role = await _userManager.IsInRoleAsync(user, "Student");
            var appDBContext = _context.Course.Include(c => c.Category).Include(c => c.Educator);
            if (!role)
            {
                var course = appDBContext.Where(x => x.Educator.UserId == userId);
                return View(await course.ToListAsync());
            }
            return View(await appDBContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Category)
                .Include(c => c.Educator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Educator")]
        public IActionResult Create()
        {
           
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["EducatorId"] = new SelectList(_context.Educator, "Id", "NameSurname");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,CourseDescription,Code,CuotaCount,PriceDaily,VideoUrl,DocumentUrl,CategoryId,EducatorId")] Course course)
        {
          
            if (ModelState.IsValid)
            {
                var userId = UserControl();
                course.EducatorId = _context.Educator.Where(x => x.UserId == userId).Select(x => x.Id).First();
                _context.Add(course);
                await _context.SaveChangesAsync();
                
                
                var enrollment = new Enrollment();

                
                enrollment.CourseID = course.Id;
                enrollment.UserID = userId;

                _context.Add(enrollment);
                await _context.SaveChangesAsync();


                // enrollment.UserID = userId;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", course.CategoryId);
            
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", course.CategoryId);
            ViewData["EducatorId"] = new SelectList(_context.Educator, "Id", "NameSurname", course.EducatorId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,CourseDescription,Code,CuotaCount,PriceDaily,VideoUrl,DocumentUrl,CategoryId,EducatorId")] Course course)
        {
            //var userId = _context.Educator.Where(x => x.Id == course.EducatorId).Select(x => x.UserId).First();
            //if (userId==null)
            //{
            //     return Content("Size Ait Olmayan Kursta Değişiklik Yapamazsınız.");
            //}
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userInfoId = UserControl();
                    course.EducatorId = _context.Educator.Where(x => x.UserId == userInfoId).Select(x => x.Id).First();
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", course.CategoryId);
            
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Category)
                .Include(c => c.Educator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduId = _context.Course.Find(id);
            var userId = _context.Educator.Where(x => x.Id == eduId.EducatorId).Select(x => x.UserId).First();
            if (userId == null)
            {
                return Content("Size Ait Olmayan Kursta Değişiklik Yapamazsınız.");
            }
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
        public string UserControl()
        {
            string userName = User.Identity.Name;
            var userId = _context.Users.Where(x => x.Email == userName).Select(x => x.Id).First();

            return userId;
        }
    }
}
