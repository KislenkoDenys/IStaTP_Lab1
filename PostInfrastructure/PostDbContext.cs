using System;
using System.Collections.Generic;
using PostDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace PostInfrastructure;

public partial class PostDbContext : DbContext
{
    public PostDbContext()
    {
    }

    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchLocation> BranchLocations { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Courier> Couriers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Parcel> Parcels { get; set; }

    public virtual DbSet<ParcelCourier> ParcelCouriers { get; set; }

    public virtual DbSet<ParcelDimension> ParcelDimensions { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Postdb;Username=postgres;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("delivery_statuses", new[] { "assigned", "picked_up", "delivered", "failed" })
            .HasPostgresEnum("parcel_statuses", new[] { "created", "processing", "in_transit", "delivered", "cancelled" });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Branches_pkey");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.WorkingHours)
                .HasColumnType("character varying")
                .HasColumnName("working_hours");

            entity.HasOne(d => d.Location).WithMany(p => p.Branches)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("location_id");
        });

        modelBuilder.Entity<BranchLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BranchLocations_pkey");

            entity.Property(e => e.Building)
                .HasColumnType("character varying")
                .HasColumnName("building");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.PostalCode)
                .HasColumnType("character varying")
                .HasColumnName("postal_code");
            entity.Property(e => e.Street)
                .HasColumnType("character varying")
                .HasColumnName("street");

            entity.HasOne(d => d.City).WithMany(p => p.BranchLocations)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("city_id");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cities_pkey");

            entity.Property(e => e.Country)
                .HasColumnType("character varying")
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Courier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Couriers_pkey");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.Couriers)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("branch_id");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Couriers)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicle_type_id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customers_pkey");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Adress)
                .HasColumnType("character varying")
                .HasColumnName("adress");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("character varying")
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("city_id");
        });

        modelBuilder.Entity<Parcel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Parcels_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryAddress)
                .HasColumnType("character varying")
                .HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryCityId).HasColumnName("delivery_city_id");
            entity.Property(e => e.ReceiverBranchId).HasColumnName("receiver_branch_id");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderBranchId).HasColumnName("sender_branch_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.TariffId).HasColumnName("tariff_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.DeliveryCity).WithMany(p => p.Parcels)
                .HasForeignKey(d => d.DeliveryCityId)
                .HasConstraintName("delivery_city_id");

            entity.HasOne(d => d.ReceiverBranch).WithMany(p => p.ParcelReceiverBranches)
                .HasForeignKey(d => d.ReceiverBranchId)
                .HasConstraintName("receiver_branch_id");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ParcelReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receiver_id");

            entity.HasOne(d => d.SenderBranch).WithMany(p => p.ParcelSenderBranches)
                .HasForeignKey(d => d.SenderBranchId)
                .HasConstraintName("sender_branch_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.ParcelSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sender_id");

            entity.HasOne(d => d.Tariff).WithMany(p => p.Parcels)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tariff_id");
        });

        modelBuilder.Entity<ParcelCourier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ParcelCouriers_pkey");

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assigned_at");
            entity.Property(e => e.CourierId).HasColumnName("courier_id");
            entity.Property(e => e.DeliveredAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delivered_at");
            entity.Property(e => e.ParcelId).HasColumnName("parcel_id");
            entity.Property(e => e.PickedUpAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("picked_up_at");

            entity.HasOne(d => d.Courier).WithMany(p => p.ParcelCouriers)
                .HasForeignKey(d => d.CourierId)
                .HasConstraintName("courier_id");

            entity.HasOne(d => d.Parcel).WithMany(p => p.ParcelCouriers)
                .HasForeignKey(d => d.ParcelId)
                .HasConstraintName("parcel_id");
        });

        modelBuilder.Entity<ParcelDimension>(entity =>
        {
            entity.HasKey(e => e.ParcelId).HasName("ParcelDimensions_pkey");

            entity.Property(e => e.ParcelId)
                .ValueGeneratedNever()
                .HasColumnName("parcel_id");
            entity.Property(e => e.HeightCm).HasColumnName("height_cm");
            entity.Property(e => e.LengthCm).HasColumnName("length_cm");
            entity.Property(e => e.WidthCm).HasColumnName("width_cm");

            entity.HasOne(d => d.Parcel).WithOne(p => p.ParcelDimension)
                .HasForeignKey<ParcelDimension>(d => d.ParcelId)
                .HasConstraintName("parcel_id");
        });

        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tariffs_pkey");

            entity.Property(e => e.MaxVolumeCm3).HasColumnName("max_volume_cm3");
            entity.Property(e => e.MaxWeight).HasColumnName("max_weight");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PricePerKg).HasColumnName("price_per_kg");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VehicleTypes_pkey");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
