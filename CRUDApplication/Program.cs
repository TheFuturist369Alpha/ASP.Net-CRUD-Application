using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using Repositories;
using RepositoryContracts;

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
builder.Services.AddScoped<IPersonRepo, PersonRepo>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPersonAddService, AddPersonService>();
builder.Services.AddScoped<IPersonDeleteService, DeletePersonService>();
builder.Services.AddScoped<IPersonGetAllService, GetAllPersonService>();
builder.Services.AddScoped<IPersonGetByIdService, GetByIdPersonService>();
builder.Services.AddScoped<IPersonSearchService, SearchPersonService>();
builder.Services.AddScoped<IPersonSortedService, SortedPersonService>();
builder.Services.AddScoped<IPersonUpdateService, UpdatedPersonService>();
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
