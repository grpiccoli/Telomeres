using FlowWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Telomeres.Data;
using Telomeres.Interfaces;

namespace Telomeres.Controllers
{
    [Authorize]
    public class UploadFileController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        readonly IBufferedFileUploadService _bufferedFileUploadService;


        public UploadFileController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IBufferedFileUploadService bufferedFileUpload)
        {
            _context = context;
            _userManager = userManager;
            _bufferedFileUploadService = bufferedFileUpload;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            Console.WriteLine("post uploadfile");
            string temporal_filename = Guid.NewGuid().ToString();
            Console.WriteLine($"{file.FileName} -> {temporal_filename}");

            try
            {
                if (await _bufferedFileUploadService.UploadFile(file, temporal_filename))
                {
                    ViewBag.Message = "File uploaded successful";

                    /*registre a filename*/
                    Report report = new Report
                    {
                        RepoUploadedFilename = temporal_filename,
                        RepoDownloadFilename = temporal_filename,
                        ApplicationUser = await _userManager.FindByEmailAsync(User.Identity?.Name)
                    };
                    //report.ApplicationUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());

                    _context.Add(report);
                    _context.SaveChanges();


                    return RedirectToAction("Index", "Reports");
                }
                else
                {
                    ViewBag.Message = "File uploaded failed";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("Index");

        }





    }


}
