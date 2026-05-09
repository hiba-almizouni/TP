using Microsoft.EntityFrameworkCore;
using Hotellerie_Hiba.Models.HotellerieModel;

var builder = WebApplication.CreateBuilder(args);

// Enregistrement du DbContext avec SQL Server
builder.Services.AddDbContext<HotellerieDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("HotellerieConnection")));

// Ajout des services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route par défaut → HotelsController.Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hotels}/{action=Index}/{id?}");

app.Run();
