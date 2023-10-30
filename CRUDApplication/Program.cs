using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DBDemoDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
}


);

/*
 *Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=People_List;IntegratedSecurity=True;
 *ConnectTimeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
 *
 */



builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ValidationHelper, ValidationHelper>();
var app = builder.Build();
if (app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
    Console.WriteLine("Exception page activated");
}
app.Logger.LogDebug("log---Debugging");
app.Logger.LogInformation("log---Information");
app.Logger.LogWarning("log---Warning");
app.Logger.LogError("log---Error");
app.Logger.LogCritical("log---Critical");
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
