using EY.CMS.CORE.IUnitOfWork;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Repositories;
using EY.CMS.CORE.Services;
using EY.CMS.REPOSITORY;
using EY.CMS.REPOSITORY.Repositories;
using EY.CMS.REPOSITORY.UnitOfWorks;
using EY.CMS.SERVICE.Mapping;
using EY.CMS.SERVICE.Services;
using EY.CMS.WEB;
using EY.CMS.WEB.CustomValidator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr-TR");

    CultureInfo[] cultures = new CultureInfo[]
    {
        new("tr-TR"),
        new("en-US"),
        new("ru-RU")
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IBlogRespository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddAutoMapper(typeof(MapProfile));
//builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();

builder.Services.AddDbContext<EyCmsContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(EyCmsContext)).GetName().Name);
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 4;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
}).AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<EyCmsContext>();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "EyCmsProject";
    opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.ExpireTimeSpan = TimeSpan.FromDays(365);
    opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
    opt.LoginPath = "/admin/login";
    opt.AccessDeniedPath = new PathString("/AccessDenied");
}

);



var app = builder.Build();
Configure(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRequestLocalization();
//app.UseRequestLocalizationCookies();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
 );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



void Configure(WebApplication host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        IdentityInitializer.SeedData(userManager, roleManager).Wait();

    }
    catch (Exception ex)
    {
        //Log some error
        throw;
    }

}


app.Run();
