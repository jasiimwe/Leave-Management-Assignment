using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //options.UseInMemoryDatabase("leave-maangement-in-memory");
    options.UseSqlite("Filename=MyDatabase.db");


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    

    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var serviceScope = serviceScopeFactory.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
    }

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();

