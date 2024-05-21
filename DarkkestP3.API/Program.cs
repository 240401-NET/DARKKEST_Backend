using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Darkkest.API.Model;
using Darkkest.API.DB;
using Darkkest.API.Service;
using Darkkest.API.Repository;

var builder = WebApplication.CreateBuilder(args);

//Add DB context and connection string
builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("darkkestDB")));

builder.Services.AddDbContext<CommunityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("darkkestDB")));

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<UserDBContext>()
.AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddHttpClient();
builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

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

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();