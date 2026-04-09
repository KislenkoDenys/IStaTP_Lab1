using Microsoft.EntityFrameworkCore;
using PostInfrastructure;
using Npgsql.NameTranslation;
using PostDomain.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.MapEnum<CourierStatus>("delivery_statuses"/*, nameTranslator: new NpgsqlNullNameTranslator()*/)
              .MapEnum<ParcelStatus>("parcel_statuses"/*, nameTranslator: new NpgsqlNullNameTranslator()*/)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cities}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
