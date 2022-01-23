using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProductService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<DataSeeder>();
//Add Repository Pattern
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
  SeedData(app);

//Seed Data
void SeedData(IHost app)
{
  var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

  using (var scope = scopedFactory.CreateScope())
  {
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service.Seed();
  }
}

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseSwagger(x => x.SerializeAsV2 = true);

app.MapGet("/product/{id}", ([FromServices] IDataRepository db, Int64 id) =>
{
  return db.GetProductById(id);
});

app.MapGet("/products", ([FromServices] IDataRepository db) =>
    {
      return db.GetProducts();
    }
);

app.MapPut("/product/{id}", ([FromServices] IDataRepository db, Product product) =>
{
  return db.PutProduct(product);
});

app.MapPost("/product", ([FromServices] IDataRepository db, Product product) =>
{
  return db.AddProduct(product);
});

app.Run();
