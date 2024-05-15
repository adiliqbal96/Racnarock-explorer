using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Adds and configures session services
builder.Services.AddDistributedMemoryCache(); // This is necessary for storing session state in-memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sets session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Makes the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Make sure this is placed after UseRouting and before UseEndpoints

app.MapRazorPages();

app.Run();
