using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHouse;
using MyHouse.Data;
using MyHouse.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//gravei na build a interface 
builder.Services.AddScoped<ILoginService, LoginService>();

// Injeção do JsonDict de Perguntas
builder.Services.AddScoped<JsonDict<long, Pergunta>>(sp => new JsonDict<long, Pergunta>("perguntas.json"));

// Injeção do RespostaService
builder.Services.AddScoped<IRespostaService, RespostaService>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddSession(); // Habilita o uso de sess�es
builder.Services.AddHttpContextAccessor(); // Permite acessar HttpContext manualmente, se necess�rio

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ATIVA o middleware de sess�o

app.UseAuthorization();

app.MapRazorPages();

app.Run();
