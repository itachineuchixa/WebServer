using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebServer;

public partial class WeatherContext : DbContext
{
    private static WeatherContext _context;
    public WeatherContext()
    {
    }

    public static WeatherContext GetContext()
    {
        if (_context == null)
            _context = new WeatherContext();
        return _context;
    }

    public WeatherContext(DbContextOptions<WeatherContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CityInfo> CityInfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=weather;Username=postgres;Password=1234", x => x.UseNodaTime());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City1)
                .HasMaxLength(89)
                .HasColumnName("city");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
        });

        modelBuilder.Entity<CityInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_info_pkey");

            entity.ToTable("city_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CurrentWeather).HasColumnName("current_weather");
            entity.Property(e => e.MaxWeather).HasColumnName("max_weather");
            entity.Property(e => e.MinWeather).HasColumnName("min_weather");
            entity.Property(e => e.PeriodWeather).HasColumnName("period_weather");

            entity.HasOne(d => d.City).WithMany(p => p.CityInfos)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_info_city_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_info_pkey");

            entity.ToTable("user_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_info_user_id_fkey");

            entity.Property(e => e.CityId).HasColumnName("city_id");

            entity.HasOne(d => d.City).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_info_city_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
