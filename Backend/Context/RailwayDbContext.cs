using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class RailwayDbContext : IdentityDbContext<ApplicationUser>
{
    public RailwayDbContext(DbContextOptions<RailwayDbContext> options) : base(options) { }

    public DbSet<Train> Trains { get; set; }
    public DbSet<TrainSchedule> TrainSchedules { get; set; }
    public DbSet<Fare> Fares { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<PassengerDetail> PassengerDetails { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }

    // 🎉 NEW FEATURE MODELS
    public DbSet<CateringOrder> CateringOrders { get; set; }
    public DbSet<WellnessKitRequest> WellnessKitRequests { get; set; }
    public DbSet<StationServiceAvailability> StationServiceAvailabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.PassengerDetails)
            .WithOne(p => p.ApplicationUser)
            .HasForeignKey(p => p.ApplicationUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Train>()
            .HasMany(t => t.Schedules)
            .WithOne(s => s.Train)
            .HasForeignKey(s => s.TrainId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Train>()
            .HasMany(t => t.Fares)
            .WithOne(f => f.Train)
            .HasForeignKey(f => f.TrainId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
             .HasMany(r => r.Passengers)
             .WithOne(p => p.Reservation)
             .HasForeignKey(p => p.ReservationId)
             .IsRequired(false) // 👈 THIS tells EF the FK is optional
             .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Payment)
            .WithOne(p => p.Reservation)
            .HasForeignKey<PaymentDetail>(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.CateringOrders)
        .WithOne(c => c.ApplicationUser)
        .HasForeignKey(c => c.ApplicationUserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.WellnessKitRequests)
        .WithOne(w => w.ApplicationUser)
        .HasForeignKey(w => w.ApplicationUserId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}