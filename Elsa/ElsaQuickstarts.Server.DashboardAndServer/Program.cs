using System.Reflection;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using ElsaQuickstarts.Server.DashboardAndServer;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using MyActivityLibrary.Activities;
using MyActivityLibrary.Bookmarks;
using MyActivityLibrary.JavaScript;
using MyActivityLibrary.Liquid;
using MyActivityLibrary.Services;

var builder = WebApplication.CreateBuilder(args);
var elsaSection = builder.Configuration.GetSection("Elsa");
builder.Services.AddElsa(elsa =>
    elsa
        .UseEntityFrameworkPersistence(ef => ef.UseSqlite())
        .AddConsoleActivities()
        .AddHttpActivities(elsaSection.GetSection("Server").Bind)
        .AddEmailActivities(options =>
        {
            options.Host = "smtp.qq.com";
            options.Port = 465;
            options.UserName = "757983029@qq.com";
            options.Password = "bievnbebfvnnbege";
            options.UseDefaultCredentials = false;
        })
        .AddQuartzTemporalActivities()
        .AddActivity<FileReceived>()
        .AddWorkflowsFrom(typeof(Program).Assembly));


builder.Services.AddBookmarkProvider<FileReceivedBookmarkProvider>();
builder.Services.AddScoped<IFileReceivedInvoker, FileReceivedInvoker>();
builder.Services.AddNotificationHandlersFrom<LiquidHandler>();
builder.Services.AddJavaScriptTypeDefinitionProvider<MyTypeDefinitionProvider>();
builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
builder.Services.AddHostedService<FileMonitorService>();

builder.Services.AddElsaApiEndpoints();
builder.Services.AddRazorPages();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles()
    .UseHttpActivities()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToPage("/_Host");
    });

app.Run();
