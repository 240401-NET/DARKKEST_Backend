using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DarkkestP3.API.Model;
using DarkkestP3.API.DB;
using DarkkestP3.API.Service;
using DarkkestP3.API.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddHttpLogging(options =>
// {
//     options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders;
// });

// builder.Services.Configure<ForwardedHeadersOptions>(options =>
// {
//     options.ForwardedHeaders =
//         ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
// });

builder.Services.AddCors(co => {
    co.AddPolicy("local" , pb =>{
        pb.WithOrigins("http://localhost:5173", "https://ambitious-plant-01ae4d90f.5.azurestaticapps.net", "https://darkkestp3.azurewebsites.net", "http://darkkestp3.azurewebsites.net")
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

// builder.Services.ConfigureApplicationCookie(options =>
// {
//     options.LoginPath = "/login";
// });

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

// app.Use((context, next) =>
// {
//     context.Request.Scheme = "https";
//     return next(context);
// });

// app.UseForwardedHeaders();

// app.UseHttpLogging();

// app.Use(async (context, next) =>
// {
//     // Connection: RemoteIp
//     app.Logger.LogInformation("Request RemoteIp: {RemoteIpAddress}",
//         context.Connection.RemoteIpAddress);

//     await next(context);
// });

// app.UseCookieAuthentication(new CookieAuthenticationOptions
// {
//     AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
//     LoginPath = new PathString("/Account/Login"),
//     Provider = cookieProvider
// });

//app.UsePathBase("/");
app.UseHttpsRedirection();
app.UseCors("local");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();