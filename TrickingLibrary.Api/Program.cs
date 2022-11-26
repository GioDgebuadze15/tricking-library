using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
    }); 
});     
builder.Services.AddControllers();
        
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();