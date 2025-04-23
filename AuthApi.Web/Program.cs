using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthApi.Application.Services;
using AuthApi.Domain.Interfaces;
using AuthApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura autenticação com JWT
var jwtSecret = builder.Configuration["Jwt:Secret"];
builder.Services.AddAuthentication(options =>
{
    // Define o esquema padrão como JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configura parâmetros de validação do token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Não valida emissor
        ValidateAudience = false, // Não valida audiência
        ValidateLifetime = true, // Valida expiração
        ValidateIssuerSigningKey = true, // Valida chave de assinatura
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

// Configura injeção de dependência
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepositoryMemory>();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    // Habilita Swagger para documentação da API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita redirecionamento para HTTPS
app.UseHttpsRedirection();

// Habilita autenticação
app.UseAuthentication();

// Habilita autorização
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplicação
app.Run();