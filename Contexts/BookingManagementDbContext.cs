using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Relation Between University & Education (One To Many)
        builder.Entity<Education>()
            .HasOne(u => u.University)
            .WithMany(e => e.Educations)
            .HasForeignKey(e => e.UniversityGuid);

        // Relation Between Employee & Education (One To One)
        builder.Entity<Education>()
            .HasOne(e => e.Employee)
            .WithOne(e => e.Education)
            .HasForeignKey<Education>(e => e.Guid);

        // Relation Between Account & Employee (One To One)
        builder.Entity<Account>()
            .HasOne(e => e.Employee)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(a => a.Guid);

        // Relation Between Account & Account Role  (One To Many)
        builder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(a => a.AccountGuid);

        // Relation Between Account Role & Role (One To Many)
        builder.Entity<AccountRole>()
            .HasOne(a => a.Role)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(a => a.RoleGuid);

        // Relation Booking & Room (Many To One)
        builder.Entity<Booking>()
            .HasOne(a => a.Room)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.RoomGuid);

        // Relation Education & University (Many To One)
        builder.Entity<Education>()
            .HasOne(u => u.University)
            .WithMany(e => e.Educations)
            .HasForeignKey(e => e.UniversityGuid);
    }
}


