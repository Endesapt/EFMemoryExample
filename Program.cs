using EFMemoryExample;
using EFMemoryExample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add database
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    //Change SQL connect string for your local db
    options.UseMySQL("server = localhost; user = root; database = test_db; password =admin;");
}
);
//Add worker
builder.Services.AddHostedService<RefreshWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
