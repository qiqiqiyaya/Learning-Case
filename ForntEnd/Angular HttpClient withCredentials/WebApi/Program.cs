using System.Collections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            // add this line
            .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// add this line
app.UseCors();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapGet("/getCookie", context =>
{
    context.Response.Cookies.Append("test", "test",new CookieOptions()
    {
        SameSite = SameSiteMode.None,
        Domain = "",
        HttpOnly = false,
        Secure = true,
    });
    return Task.CompletedTask;
});
app.MapGet("/containCookie", context =>
{
    if (context.Request.Cookies.ContainsKey("test"))
    {
        return context.Response.WriteAsync("yes");
    }

    return context.Response.WriteAsync("no");
});
app.Run();
