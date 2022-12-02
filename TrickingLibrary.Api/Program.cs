using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
    });
});
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    if (env.IsDevelopment())
    {
        ctx.Add(new Difficulty {Id = "easy", Name = "Easy", Description = "Easy Test"});
        ctx.Add(new Difficulty {Id = "medium", Name = "Medium", Description = "Medium Test"});
        ctx.Add(new Difficulty {Id = "hard", Name = "Hard", Description = "Hard Test"});
        ctx.Add(new Category {Id = "kick", Name = "Kick", Description = "Kick Test"});
        ctx.Add(new Category {Id = "flip", Name = "Flip", Description = "Flip Test"});
        ctx.Add(new Category {Id = "transition", Name = "Transition", Description = "Transition Test"});
        ctx.Add(new Trick
        {
            Id = "backwards-roll",
            Name = "Backwards Roll",
            Description = "This is a test backwards roll",
            Difficulty = "easy",
            TrickCategories = new List<TrickCategory>
            {
                new TrickCategory {CategoryId = "flip"}
            }
        });
        ctx.Add(new Trick
        {
            Id = "back-flip",
            Name = "Back Flip",
            Description = "This is a test back flip",
            Difficulty = "medium",
            TrickCategories = new List<TrickCategory>
            {
                new TrickCategory {CategoryId = "flip"}
            },
            Prerequisites = new List<TrickRelationship>
            {
                new TrickRelationship{PrerequisiteId = "backwards-roll"}
            }
        });
        ctx.SaveChanges();
    }
}


app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

app.Run();