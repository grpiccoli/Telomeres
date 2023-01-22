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

            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "File uploaded successful";
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
