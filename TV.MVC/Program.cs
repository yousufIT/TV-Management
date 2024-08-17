using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using TV.Infrastructure;
using TV.Infrastructure.Repositories;
using TV.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddXmlDataContractSerializerFormatters()
    .AddNewtonsoftJson();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//// Configure Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
//    options.SignIn.RequireConfirmedAccount = true;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//builder.Services.AddSingleton<IEmailSender, EmailSender>();


// Register repositories
builder.Services.AddScoped<ITVShowRepository, TVShowRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<ILanguagesRepository, LanguagesRepository>();
builder.Services.AddScoped<ILanguageTVShowRepository, LanguageTVShowRepository>();

// Add Razor Pages services
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TVShows}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    ApplicationDbContext.CreatingInitialTestingDatabase(context);
}
// Add Razor Pages routing
app.MapRazorPages();

app.Run();
