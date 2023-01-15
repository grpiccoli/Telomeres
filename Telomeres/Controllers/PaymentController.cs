﻿using FlowWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telomeres.Data;

namespace FlowWebApp.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IFlow _flow;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PaymentController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IFlow flow)
        {
            _userManager = userManager;
            _flow = flow;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            Payment model = new()
            {
                Id = 0,
                Price = 100000,
                PeriodDate = DateTime.Now
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Pay(int Id)
        {
            Payment payment = new();
            string email = string.Empty;
            if (Id == 0)
            {
                payment.Id = DateTime.Now.Millisecond;
                payment.Price = 100000;
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity?.Name).ConfigureAwait(false);
                email = user.Email;
            }
            else
            {
                payment = await _context.Payments
                    .Include(p => p.Report)
                        .ThenInclude(b => b.ApplicationUser)
                    .FirstAsync(p => p.Id == Id).ConfigureAwait(false);
                email = payment.Report?.ApplicationUser?.Email ?? string.Empty;
            }
            string url = _flow.PaymentCreate(
                payment.Id, "PagoParticular",
                payment.Price,
                email);
            return Redirect(url);
        }
    }
}
