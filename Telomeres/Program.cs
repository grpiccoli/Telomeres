using Flow;
using FlowWebApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Telomeres.Data;
using Telomeres.Interfaces;
using Telomeres.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped(x =>
{
    ActionContext? actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    if (actionContext == null) throw new NullReferenceException(nameof(actionContext));
    IUrlHelperFactory factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

builder.Services.Configure<FlowSettings>(o =>
{
    bool dev = !args.Any(a => a == "flow");
    string flowEnv = dev ? "Sandbox" : "Production";
    string preffix = dev ? "sandbox" : "www";
    o.ApiKey = builder.Configuration[$"Flow:{flowEnv}:ApiKey"];
    o.SecretKey = builder.Configuration[$"Flow:{flowEnv}:SecretKey"];
    o.Currency = "CLP";
    o.EndPoint = new Uri($"https://{preffix}.flow.cl/api");
});
builder.Services.AddScoped<IFlow, FlowService>();
builder.Services.AddScoped<IBufferedFileUploadService, BufferedFileUploadLocalServices>(); /*upload file service*/
builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "__RequestVerificationToken";
    options.HeaderName = "X-CSRF-TOKEN";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
