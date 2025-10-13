using DependencyInjection.Interface;
using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services to the container
builder.Services.AddControllersWithViews();

// ✅ Register DI services BEFORE building the app
builder.Services.AddScoped<IGreeter, MeetingMessage>();
builder.Services.AddScoped<IGreeter, WeekendMeeting>();
var app = builder.Build();

// ✅ Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ✅ serve CSS, JS, etc.

app.UseRouting();
app.UseAuthorization();

// ✅ Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
