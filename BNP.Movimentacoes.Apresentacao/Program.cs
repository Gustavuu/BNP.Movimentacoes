using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Aplicacao.Services;
using BNP.Movimentacoes.Infraestrutura.Contexto;
using BNP.Movimentacoes.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona o suporte ao Swagger para documentar nossa API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o DbContext do Entity Framework para usar o SQL Server LocalDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovimentacoesDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registra nossas interfaces e suas implementações concretas.
// Usamos AddScoped para que a instância desses serviços dure por uma requisição HTTP.
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IMovimentoManualRepository, MovimentoManualRepository>();
builder.Services.AddScoped<IMovimentoManualService, MovimentoManualService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Aplica as migrations do EF Core automaticamente ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MovimentacoesDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro durante a aplicação da migration.");
    }
}

app.Run();
