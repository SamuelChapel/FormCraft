using FormCraft.Business.Extensions;
using FormCraft.Entities;
using FormCraft.Repositories.Database.Contexts;
using FormCraft.Repositories.Extensions;
using FormCraft.WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddWebApp()
        .AddBusiness()
        .AddRepositories(builder.Configuration);
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddControllersWithViews();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.AddMigrations();
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

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}