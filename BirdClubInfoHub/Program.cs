using BirdClubInfoHub.Data;
using BirdClubInfoHub.Middlewares;
using BirdClubInfoHub.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BcmsDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IAccountGenerationService, AccountGenerationService>();
builder.Services.AddFluentEmail(config.GetSection("Mail")["Sender"], config.GetSection("Mail")["From"])
    .AddRazorRenderer()
    .AddSmtpSender(new SmtpClient(config.GetSection("Mail")["Host"])
    {
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Port = 25
    });
builder.Services.AddSession();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStatusCodePagesWithRedirects("/StatusCodeError/{0}");

app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
