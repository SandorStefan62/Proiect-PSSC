using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proiect.Data;
using Proiect.Data.Repository;
using Proiect.Domain.Repository;
using Proiect.Domain.Workflows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
;
builder.Services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository,ProductRepository>();
builder.Services.AddTransient<IOrderHeaderRepository, OrderHeaderRepository>();
builder.Services.AddTransient<IOrderLineRepository, OrderLineRepository>();
builder.Services.AddTransient<ValidationWorkflow>();
builder.Services.AddTransient<CalculateOrderWorkflow>();
builder.Services.AddTransient<FinishOrderWorkflow>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
