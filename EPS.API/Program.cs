using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EPS.Service.Helpers;
using EPS.Service;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EPS.API.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Net;
using HotChocolate;
using EPS.Data.SolrEntities;
using SolrNet;
using EPS.Utils.Service;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(EPSBaseService));
builder.Services.AddHostedService(provider =>
{
    var key = configuration["JWT:Key"];
    return new EPSService { _keyPrivate=key};
});
// For Entity Framework
builder.Services.AddDbContext<EPSContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("EPS")));
builder.Services.AddDataProtection()
                .PersistKeysToDbContext<EPSContext>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddSingleton<EPSService>();
builder.Services.AddSingleton<IJWTServices, JWTServices>();

builder.Services.Configure<Audience>(configuration.GetSection("JWT"));

// For Identity
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<EPSContext>()
  .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 3;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
    // User settings
    //options.User.RequireUniqueEmail = true;
});
builder.Services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
builder.Services.AddScoped<IUserIdentity<int>, UserIdentity>();
builder.Services.AddScoped<EPSRepository>();
builder.Services.AddScoped<EPSBaseService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<SolrServices<SolrLogs>>();
builder.Services.AddSolrNet<SolrLogs>(configuration.GetConnectionString("URLSolrLogs"));
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    //options.SaveToken = true;
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
        ValidateLifetime = true,
        

        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == (int)HttpStatusCode.OK)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }
        return Task.FromResult(0);
    };
});
builder.Services.AddQueryRequestInterceptor((context, builder, ct) =>
{
    if (context.User.Identity.IsAuthenticated)
    {

        builder.AddProperty(
            "httpContext",
            context);
    }
    return Task.CompletedTask;
});
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
DbInitializer.Initialize(app.Services);
app.Run();
