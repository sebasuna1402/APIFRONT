using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApiService;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add External Dependencies
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

//JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:SecretKey"])),
        ValidateIssuer = false,
        ValidIssuer = "test",
        ValidateAudience = false,
        ValidAudience = "test",
        RequireExpirationTime = false,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromDays(1),
    };
});

//Seeder Service
builder.Services.AddScoped<DataSeeder>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

//app.UseAuthentication();
app.UseRouting();
//app.UseAuthorization();

//Seeding Data for Testing
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetRequiredService<DataSeeder>;
    services.Invoke().Seed();
}

app.MapControllers();

app.Run();
