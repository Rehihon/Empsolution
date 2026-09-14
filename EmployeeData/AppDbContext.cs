using EmployeeDomain;
using Microsoft.EntityFrameworkCore;
namespace EmployeeData;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DateOnly DateOfBirth { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .ToTable("Contact");

        modelBuilder.Entity<Jobhistory>()
            .ToTable("JobHistory");


    }

    public DbSet<Employee> Employees => Set<Employee>();
     public DbSet<Address> Address => Set<Address>();
      public DbSet<Countries> Countries => Set<Countries>();
      public DbSet<Jobs> Jobs => Set<Jobs>();
      public DbSet<Jobhistory> Jobhistory => Set<Jobhistory>();
      public DbSet<Department> Department => Set<Department>();
    public DbSet<Contact> Contact => Set<Contact>();
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //         "Server=DESKTOP-8RH9LPT;Database=EmpslnDB;Trusted_Connection=True;TrustServerCertificate=True;");
    // }

}