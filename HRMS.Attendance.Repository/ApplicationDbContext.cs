using HRMS.Attendance.AggregrateRoot.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
 
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<ManageAttendance> ManageAttendences { get; set; }
    public DbSet<Holiday> Holidays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveRequest>()
            .HasOne(lr => lr.Employee)
            .WithMany(e => e.LeaveRequests)
            .HasForeignKey(lr => lr.EmployeeID);

        // Additional configurations if needed
        base.OnModelCreating(modelBuilder);
    }
}
