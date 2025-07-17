using LJA.FinancialTransaction.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// The MigrationAssembly needs to be in the same project as the DbContext file
builder.Services.AddDbContext<FinancialTransactionDbContext>(options =>
{
    // Change DbContextBuilder to point to the assembly with DBContext 
    options.UseSqlServer(builder.Configuration.GetConnectionString("LJAConnection"),
         b => b.MigrationsAssembly("LJA.FinancialTransaction.Persistence"));
});

var app = builder.Build();

//Auto-migrate database on startup.  I wouldn't use this for production
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FinancialTransactionDbContext>();
    dbContext.Database.Migrate();
}

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
