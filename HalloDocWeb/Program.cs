using HalloDocWebRepository.Data;
using HalloDocWebRepository.Implementation;
using HalloDocWebRepository.Interfaces;
using HalloDocWebServices.Implementation;
using HalloDocWebServices.Interfaces;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
builder.Services.AddScoped<IJwt_Service, Jwt_Service>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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