using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DarkkestP3.API.Model;
using DarkkestP3.API.DB;
using DarkkestP3.API.Service;
using DarkkestP3.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(co => {
    co.AddPolicy("local" , pb =>{
        pb.WithOrigins("http://localhost:5173", "https://ambitious-plant-01ae4d90f.5.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//Add DB context and connection string
builder.Services.AddDbContext<UserDBContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("darkkestDB")));

builder.Services.AddDbContext<CommunityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("darkkestDB")));

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();

// Add repos to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<UserDBContext>()
.AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
});

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

app.UseCors("local");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();