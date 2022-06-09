using Books.Api.Data;
using Books.Api.Models;
using Books.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IBooksService, BooksService>();
    services.AddTransient<IUserService, UsersService>();
    services.AddTransient<IJwtService, JwtService>();
    services.AddDbContext<BooksDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDocker")));
    services.AddIdentityCore<BooksUser>(config =>
    {
        config.User.RequireUniqueEmail = true;
        config.SignIn.RequireConfirmedEmail = true;
    })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<BooksDbContext>(); ;
}