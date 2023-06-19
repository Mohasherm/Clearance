using Clearance.Server.Data;
using Clearance.Server.Repo;
using Clearance.Server.Repo.IRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IDirectionService, DirectionService>();
builder.Services.AddScoped<ICollageService, CollageService>();
builder.Services.AddScoped<ICollageDirectionService, CollageDirectionService>();
builder.Services.AddScoped<IClearanceService, ClearanceService>();

builder.Services.AddTransient<IClaimsService, ClaimsService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));
builder.Services.AddIdentity<AppUser, AppRole>(op =>
{
    op.Password.RequireDigit = false;
    op.Password.RequiredLength = 3;
    op.Password.RequireLowercase = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireNonAlphanumeric = false;
    op.SignIn.RequireConfirmedEmail = false;
})
   .AddDefaultTokenProviders()
   .AddEntityFrameworkStores<DataContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ClockSkew = TimeSpan.Zero, // Messes with expiry!
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
