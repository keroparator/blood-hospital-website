using Microsoft.AspNetCore.Authentication.Cookies;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Supabase Configuration
var supabaseUrl = builder.Configuration["SupabaseUrl"];
var supabaseKey = builder.Configuration["SupabaseKey"];

builder.Services.AddScoped<Supabase.Client>(_ => 
    new Supabase.Client(supabaseUrl!, supabaseKey!, new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    }));

// Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// SMTP Settings Configuration (Available via IConfiguration in pages)

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
