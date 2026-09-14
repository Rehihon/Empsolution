using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;


    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
           sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("MyApi");

            } 
            )); 

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
