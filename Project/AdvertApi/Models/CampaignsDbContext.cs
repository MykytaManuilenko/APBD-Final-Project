using Microsoft.EntityFrameworkCore;

namespace AdvertApi.Models
{
    public class CampaignsDbContext : DbContext
    {
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Campaing> Campaings { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }

        public CampaignsDbContext()
        {

        }

        public CampaignsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Building>(opt =>
            {
                opt.HasKey(b => b.IdBuilding);
                opt.Property(b => b.IdBuilding)
                   .ValueGeneratedOnAdd();

                opt.Property(b => b.Street)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(b => b.City)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(b => b.Height)
                   .HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<Client>(opt =>
            {
                opt.HasKey(c => c.IdClient);
                opt.Property(c => c.IdClient)
                   .ValueGeneratedOnAdd();

                opt.Property(c => c.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(c => c.LastName)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(c => c.Email)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(c => c.Phone)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(c => c.Login)
                   .HasMaxLength(100)
                   .IsRequired();

                opt.Property(c => c.Password)
                   .IsRequired();
            });

            modelBuilder.Entity<Campaing>(opt =>
            {
                opt.HasKey(c => c.IdCampaing);
                opt.Property(c => c.IdCampaing)
                   .ValueGeneratedOnAdd();

                opt.Property(c => c.PricePerSquareMeter)
                   .HasColumnType("decimal(6, 2)");

                opt.HasOne(c => c.Client)
                   .WithMany(c => c.Campaings)
                   .HasForeignKey(c => c.IdClient)
                   .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(c => c.FromBuilding)
                   .WithMany(c => c.CampaingsFrom)
                   .HasForeignKey(c => c.FromIdBuilding)
                   .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(c => c.ToBuilding)
                   .WithMany(c => c.CampaingsTo)
                   .HasForeignKey(c => c.ToIdBuilding)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Banner>(opt =>
            {
                opt.HasKey(b => b.IdAdvertisement);
                opt.Property(b => b.IdAdvertisement)
                   .ValueGeneratedOnAdd();

                opt.Property(b => b.Price)
                   .HasColumnType("decimal(6, 2)");

                opt.Property(b => b.Area)
                   .HasColumnType("decimal(6, 2)");

                opt.HasOne(b => b.Campaing)
                   .WithMany(b => b.Banners)
                   .HasForeignKey(b => b.IdCampaing)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
