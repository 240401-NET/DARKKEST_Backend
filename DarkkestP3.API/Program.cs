using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DarkkestP3.API.Model;
using DarkkestP3.API.DB;
using DarkkestP3.API.Service;
using DarkkestP3.API.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddHttpLogging(options =>
// {
//     options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders;
// });

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear(); // Clear default networks
    options.KnownProxies.Clear(); // Clear default proxies
});

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

app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
});

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
//app.UseHsts();
app.UseHttpsRedirection();
app.UseCors("local");

// app.Use(async (context, next) =>
// {
//     if (context.Request.Method == HttpMethods.Options)
//     {
//         var requestOrigin = context.Request.Headers["Origin"];
//         if (requestOrigin == "http://localhost:5173" || requestOrigin == "https://ambitious-plant-01ae4d90f.5.azurestaticapps.net")
//         {
//             context.Response.Headers.Append("Access-Control-Allow-Origin", requestOrigin);
//             context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
//             context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
//             context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
//             context.Response.StatusCode = 204; // No Content
//         }
//         await context.Response.CompleteAsync();
//     }
//     else
//     {
//         await next();
//     }
// });

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();