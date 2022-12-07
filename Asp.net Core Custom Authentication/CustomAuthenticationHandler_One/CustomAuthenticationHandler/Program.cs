using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CustomAuthConfiguration.CustomAuthScheme)
    .AddCookie(CustomAuthConfiguration.CustomAuthScheme, "test", options =>
    {
        options.AccessDeniedPath = "/AccessDenied";
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";

        options.Events.OnSigningIn = context =>
        {
            var test = context;
            return Task.CompletedTask;
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/AccessDenied", () => "Access denied");
app.MapGet("/Login", async (context) =>
{
    var identity = new ClaimsIdentity(null, "pwd");
    identity.AddClaim(new Claim(ClaimTypes.Name, "haha"));
    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
    identity.AddClaim(new Claim("sub", "adsfdsafdsfdssafdas"));
    var cp = new ClaimsPrincipal(identity);

    var schemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
    var allSchemes = await schemeProvider.GetAllSchemesAsync();

    await context.SignInAsync(CustomAuthConfiguration.CustomAuthScheme, cp, new AuthenticationProperties()
    {
        IsPersistent = true,
    });
});
app.MapGet("/Logout", async context =>
{
    await context.SignOutAsync(CustomAuthConfiguration.CustomAuthScheme);
});
app.MapGet("/auth", Auth);

app.UseAuthentication();
app.UseAuthorization();

app.Run();

[Authorize]
async Task Auth(HttpContext context)
{
    //await context.Response.WriteAsync("admin page");
    await context.Response.WriteAsJsonAsync(context.User.Identity);
}


class CustomAuthConfiguration
{
    public const string CustomAuthScheme = "CustomAuthScheme";
}