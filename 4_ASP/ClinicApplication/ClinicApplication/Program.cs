using ClinicApplication.Infrastructure.Data;
using ClinicApplication.Infrastructure.Repositories;
using ClinicApplication.Infrastructure.Repositories.Base;
using ClinicApplication.Application.Interfaces;
using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using ClinicApplication.Application.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(conectionString));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialtyRepository>();
builder.Services.AddScoped<IMedical_RecordsRepository, Medical_RecordsRepository>();
builder.Services.AddScoped<IPrescriptionsRepository, PrescriptionsRepository>();
builder.Services.AddScoped<IApointmentRepository, ApointmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();




builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedical_RecordsService, Medical_RecordsService>();
builder.Services.AddScoped<IPrescriptionsService, PrescriptionsService>();
builder.Services.AddScoped<IUserService, UserService>();

















// ================================ 
// Cookie Authentication 
// ================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=login}/{id?}");

app.Run();
