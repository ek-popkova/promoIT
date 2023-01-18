using Microsoft.EntityFrameworkCore;
using promoit_frontend_cs.Data;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using promoit_frontend_cs.Services;
using Microsoft.JSInterop;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using promoit_frontend_cs.Pages.SocialActivist;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<promoit_backend_cs_api.Data.promo_itContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpClient();
/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7000/") });
*/
builder.Services.AddScoped<PopupService>();
builder.Services.AddScoped<BusinessCompanyRepresentativeService>();
builder.Services.AddScoped<CampaignService>();
builder.Services.AddScoped<SocialActivistService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddLogging();
//builder.Services.AddSingleton<BalanceAndProduct>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
        options.Scope = "openid profile email";
        options.OpenIdConnectEvents = new OpenIdConnectEvents
        {
            OnTokenValidated = (context) =>
            {
                var token = context.SecurityToken.RawHeader + "." +
                    context.SecurityToken.RawPayload + "." + context.SecurityToken.RawSignature;
                context.Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
    //            var refresh_token = context.TokenEndpointResponse.RefreshToken;
    //            context.Response.Cookies.Append("refresh_token", refresh_token, new CookieOptions
    //            {
				//	HttpOnly = true,
				//	Secure = true,
				//	SameSite = SameSiteMode.Strict
				//});


                return Task.CompletedTask;
            },
        };

    }).WithAccessToken(options =>
    {
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.UseRefreshTokens = true;
    });


builder.Services.AddAuthorization(options => {
    options.AddPolicy("NoRolePolicy", policy => policy.RequireAssertion(context => !context.User.HasClaim(c => c.Type == ClaimTypes.Role)));
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddHttpClient();

var app = builder.Build();
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("")
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();



app.UseStaticFiles();

app.UseRouting();
app.Use((context, next) => { context.Request.Scheme = "https"; return next(); });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
