using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Helpers;
using SalesOrder.Interfaces;
using SalesOrder.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SalesOrder}/{action=Index}/{id?}");


//string connectionString = "Server=ITDEV04,1433;Database=SalesOrder;User Id=sa;Password=password_baru;TrustServerCertificate=True";

//// Membuat objek koneksi
//using (SqlConnection conn = new SqlConnection(connectionString))
//{
//    try
//    {
//        // Membuka koneksi
//        conn.Open();
//        Console.WriteLine("Koneksi berhasil.");
//    }
//    catch (SqlException ex)
//    {
//        Console.WriteLine("Terjadi kesalahan: " + ex.Message);
//    }
//}

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();