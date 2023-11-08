using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using fiducial.web.Data;
using fiducial.dal;
using fiducial.dal.Configuration;
using fiducial.dal.Repositories.Book;
using fiducial.dal.Factories;
using fiducial.dal.Seeds;
using fiducial.bll.Services.Book;
using fiducial.bll.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

    //seeds
    builder.Services.AddSingleton<CreateTables>();

    //configuration mapping
    builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

    //repositories
    builder.Services.AddScoped<IBookRepository, BookRepository>();

    //services
    builder.Services.AddScoped<IBookService, BookService>();

    //automapper profiles
    builder.Services.AddAutoMapper(typeof(AutomapperProfileBll));
}

var app = builder.Build();

//database initialization
{
    using var scope = app.Services.CreateScope();
    var createTablesSeed = scope.ServiceProvider.GetRequiredService<CreateTables>();
    await createTablesSeed.Init();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
