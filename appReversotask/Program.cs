using Microsoft.EntityFrameworkCore;
using appReversotask.Models; // Ajuste o namespace se o nome do projeto for diferente

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registro do DbContext
builder.Services.AddDbContext<dbTasksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoDefault")));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
