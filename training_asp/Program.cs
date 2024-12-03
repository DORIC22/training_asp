using Microsoft.EntityFrameworkCore;
using training_asp.Data;
using Microsoft.AspNetCore.Http; // �������� ��� ��� ������������� IHttpContextAccessor

var builder = WebApplication.CreateBuilder(args);

// ��������� ������
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // ������������� ����� ����� ������
    options.Cookie.HttpOnly = true; // ������������� ���� HttpOnly ��� ������������
    options.Cookie.IsEssential = true; // ����� ������ ���� �������� ���� ��� �������� �� cookies
});

// Add services to the container.
builder.Services.AddRazorPages();

// ��������� ApplicationDbContext � �������������� ������ ����������� �� appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� IHttpContextAccessor ��� �������� � PageModel
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

// ��������� ��������� ������
app.UseSession();  // �������� ������������� ������

app.UseAuthorization();

app.MapRazorPages();

app.Run();
