using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parking.Data;
using Parking.Models;
using Parking.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços da aplicação
ConfigureServices(builder.Services);

var app = builder.Build();

// Middleware de inicialização de dados
UseDatabaseSeeder(app);

// Configuração do pipeline de requisições HTTP
ConfigureHttpPipeline(app);

app.Run();

// Configuração dos serviços da aplicação
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Configurar a autenticação JWT
    var jwtOptions = builder.Configuration.GetSection("JwtOptions");
    var secretKey = jwtOptions["SecretKey"] ?? "";
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    services.Configure<JwtBearerOptions>(jwtOptions);
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "service",
                ValidAudience = "ui",
                IssuerSigningKey = key
            };
        });
    services.AddScoped<IEventService, EventService>();
}

// Middleware de inicialização de dados
async void UseDatabaseSeeder(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await Seeders.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}

// Configuração do pipeline de requisições HTTP
void ConfigureHttpPipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}
