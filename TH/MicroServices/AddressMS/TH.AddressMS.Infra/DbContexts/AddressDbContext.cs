using Microsoft.EntityFrameworkCore;
using TH.AddressMS.Core;

namespace TH.AddressMS.Infra
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext(DbContextOptions<AddressDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(450);
                entity.Property(e => e.ClientId).HasMaxLength(450);
                entity.Property(e => e.CountryId).HasMaxLength(450);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.PostalCode).HasMaxLength(450);
                entity.Property(e => e.State).HasMaxLength(450);
                entity.Property(e => e.Street).HasMaxLength(450);

                entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Addresses_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(450);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.CurrencyName).HasMaxLength(450);
                entity.Property(e => e.CurrencyCode).HasMaxLength(450);
                entity.Property(e => e.IsoCode).HasMaxLength(450);
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.Name).HasMaxLength(450);
            });
        }
    }
}