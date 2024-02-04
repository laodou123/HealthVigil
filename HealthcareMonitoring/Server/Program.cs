using HealthcareMonitoring.Server.Data;
using HealthcareMonitoring.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HealthcareMonitoring.Server.IRepository;
using HealthcareMonitoring.Server.Repository;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.StaticFiles;
using HealthcareMonitoring.Server.Hubs;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRadzenComponents();

// Add services to the container.
builder.Services.AddSignalR();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 增加 AzureOpenAI 服务
builder.Services.AddBootstrapBlazorAzureOpenAIService();

builder.Services.Configure<AzureOpenAIOption>(op =>
{
    op.Endpoint = "https://vsyishimaochengwang.openai.azure.com/";
    op.Key = "feb9cf7859fa4b828e9dec6a5534b50a";
    op.DeploymentName = "gpt-35-turbo";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".properties"] = "application/octet-stream";
provider.Mappings[".moc"] = "application/x-msdownload";
provider.Mappings[".moc3"] = "application/x-msdownload";
provider.Mappings[".mtn"] = "application/x-msdownload";

app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHub<Chathub>("/chathub");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Patient", "Doctor" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
app.Run();
