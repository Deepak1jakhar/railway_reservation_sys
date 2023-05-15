using Microsoft.EntityFrameworkCore;
using trainmodels.Data;
using trainmodels.Repository;
using trainmodels.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RailContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("RailwayConnection")
    ));
builder.Services.AddScoped<ITrain, TrainRepository>();
builder.Services.AddScoped<IPassenger, PassengerRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IBooking, BookingRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
