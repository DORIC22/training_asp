using Microsoft.EntityFrameworkCore;
using training_asp.Data;
using Microsoft.AspNetCore.Http; // Добавьте это для использования IHttpContextAccessor

var builder = WebApplication.CreateBuilder(args);

// Добавляем сессии
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Устанавливаем время жизни сессии
    options.Cookie.HttpOnly = true; // Устанавливаем флаг HttpOnly для безопасности
    options.Cookie.IsEssential = true; // Чтобы сессия была доступна даже без согласия на cookies
});

// Add services to the container.
builder.Services.AddRazorPages();

// Добавляем ApplicationDbContext с использованием строки подключения из appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем IHttpContextAccessor для инъекции в PageModel
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Добавляем поддержку сессий
app.UseSession();  // Включаем использование сессий

app.UseAuthorization();

app.MapRazorPages();

app.Run();
