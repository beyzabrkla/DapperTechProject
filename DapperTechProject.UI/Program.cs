using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.BusinessLayer.Concrete;
using DapperTechProject.BusinessLayer.Context;
using DapperTechProject.BusinessLayer.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

// Context Kaydı
builder.Services.AddSingleton<DapperContext>();

// Repository Kayıtları (Dependency Injection)
builder.Services.AddScoped<IAdImpressionRepository, AdImpressionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
