using TrickingLibrary.Api.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TrickyStore>();
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