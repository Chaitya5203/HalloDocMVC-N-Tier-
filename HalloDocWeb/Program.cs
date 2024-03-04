using HalloDocWebRepository.Data;
using HalloDocWebRepository.Implementation;
using HalloDocWebRepository.Interfaces;
using HalloDocWebServices.Implementation;
using HalloDocWebServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IPatient_Repository, Patient_Repository>();
builder.Services.AddScoped<IPatient_Service, Patient_Service>();
builder.Services.AddScoped<IAdmin_Repository, Admin_Repository>();
builder.Services.AddScoped<IAdmin_Service, Admin_Service>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
