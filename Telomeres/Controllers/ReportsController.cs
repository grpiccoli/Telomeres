using FlowWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telomeres.Data;


namespace Telomeres.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public ReportsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(User.Identity?.Name).ConfigureAwait(false);
            ViewBag.show_error = false;
            ViewBag.show_success = false;
            ViewBag.message = "";


            return View(await _context.Reports.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report)

        {
            Console.WriteLine("Ingresando en post create");
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UploadFile")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reports'  is null.");
            }
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
