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

namespace EduPortalV2.Controllers
{
    [Authorize()]
    public class MyCoursesController : Controller
    {
        private readonly AppDBContext _context;

        public MyCoursesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: MyCourses
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> Index()
        {
            
            var userId = UserControl();

            var appDBContext = _context.MyCourse.Include(m => m.Course).Include(m => m.Educator).Include(m => m.ApplicationUser);
            var myCourse = appDBContext.Where(x => x.UserId == userId);
            return View(await myCourse.ToListAsync());
        }

        // GET: MyCourses/Details/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCourse = await _context.MyCourse
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myCourse == null)
            {
                return NotFound();
            }

            return View(myCourse);
        }

        // GET: MyCourses/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName");
            return View();
        }

        // POST: MyCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,EducaterId,UserId,Statu")] MyCourse myCourse)
        {
            if (ModelState.IsValid)
            {
               
                var userId = UserControl();
                var course = _context.Course.Find(myCourse.CourseId);
                myCourse.EducaterId = course.EducatorId;
                
                myCourse.UserId = userId;
                _context.Add(myCourse);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", myCourse.CourseId);
            return View(myCourse);
        }

        // GET: MyCourses/Edit/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCourse = await _context.MyCourse.FindAsync(id);
            if (myCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", myCourse.CourseId);
            return View(myCourse);
        }

        // POST: MyCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,EducaterId,UserId,Statu")] MyCourse myCourse)
        {
            if (id != myCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var list = _context.MyCourse.Find(myCourse.Id);
                    list.CourseId = myCourse.CourseId;
                    list.Statu = myCourse.Statu;
                    var course = _context.Course.Find(myCourse.CourseId);
                    list.EducaterId = course.EducatorId;
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyCourseExists(myCourse.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", myCourse.CourseId);
            return View(myCourse);
        }

        // GET: MyCourses/Delete/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCourse = await _context.MyCourse
                .Include(m => m.Course).Include(m => m.Educator).Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myCourse == null)
            {
                return NotFound();
            }

            return View(myCourse);
        }

        // POST: MyCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myCourse = await _context.MyCourse.FindAsync(id);
            _context.MyCourse.Remove(myCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyCourseExists(int id)
        {
            return _context.MyCourse.Any(e => e.Id == id);
        }
        public string UserControl()
        {
            string userName = User.Identity.Name;
            var userId = _context.Users.Where(x => x.Email == userName).Select(x => x.Id).First();

            return userId;
        }
    }
}
