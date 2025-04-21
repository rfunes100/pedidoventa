using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediWeb.Data;
using CloudinaryDotNet;

using QuestPDF.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// Configurar la licencia de QuestPDF
QuestPDF.Settings.License = LicenseType.Community;


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MediWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MediWebContext") ?? throw new InvalidOperationException("Connection string 'MediWebContext' not found.")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");
var cloudinary = new Cloudinary(new Account(
    cloudinaryConfig["CloudName"],
    cloudinaryConfig["ApiKey"],
    cloudinaryConfig["ApiSecret"]

    ));


builder.Services.AddSingleton(cloudinary);



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
app.UseSession();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//    pattern: "{controller=Usuario}/{action=Login}/{id?}");
    pattern: "{controller=Medicamento}/{action=Lista}/{id?}");



app.Run();
