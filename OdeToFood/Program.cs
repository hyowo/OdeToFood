using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

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

SetupAppData(app, app.Environment);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    "Cuisine", "cuisine/{name}",
    new { controller = "Cuisine", action = "Search", name = "" });

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
}
);

app.MapRazorPages();

app.Run();

void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env)
{
    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope
        .ServiceProvider
        .GetService<ApplicationDbContext>();
    if (context == null)
    {
        throw new ApplicationException("Problem in services. Can not initialize ApplicationDbContext");
    }
    while (true)
    {
        try
        {
            context.Database.OpenConnection();
            context.Database.CloseConnection();
            break;
        }
        catch (SqlException e)
        {
            if (e.Message.Contains("The login failed.")) { break; }
            Thread.Sleep(1000);
        }
    }
    AppDataInit.SeedRestaurant(context);

}