using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data;

public class ApplicationDbContext : DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Person> Person { get; set;}
    public DbSet<Employee> Employees { get; set;}

public DbSet<MvcMovie.Models.Student> Student { get; set; } = default!;


}
