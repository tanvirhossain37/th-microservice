using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TH.RnD.CL.Models;

public partial class DoNotDeleteSpaceContext : DbContext
{
    public DoNotDeleteSpaceContext()
    {
    }

    public DoNotDeleteSpaceContext(DbContextOptions<DoNotDeleteSpaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchUser> BranchUsers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SpaceSubscription> SpaceSubscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCompany> UserCompanies { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DoNotDeleteSpace;User ID=sa;Password=admin123##;Trust Server Certificate=True");

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

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(256);
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.SpaceId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Branches)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branches_Companies");
        });

        modelBuilder.Entity<BranchUser>(entity =>
        {
            entity.Property(e => e.BranchId).HasMaxLength(450);
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Branch).WithMany(p => p.BranchUsers)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchUsers_Branches");

            entity.HasOne(d => d.Company).WithMany(p => p.BranchUsers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchUsers_Companies");

            entity.HasOne(d => d.User).WithMany(p => p.BranchUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchUsers_Users");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(256);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Logo).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Slogan).HasMaxLength(450);
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.Website).HasMaxLength(256);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrencyCode).HasMaxLength(450);
            entity.Property(e => e.CurrencyName).HasMaxLength(450);
            entity.Property(e => e.IsoCode).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.ControllerName).HasMaxLength(256);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(256);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.ParentId).HasMaxLength(450);
            entity.Property(e => e.Route).HasMaxLength(256);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Modules_Modules");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasMaxLength(450);
            entity.Property(e => e.ParentId).HasMaxLength(450);
            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.SpaceId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_Companies");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_Modules");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Permissions_Permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(256);
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.SpaceId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Companies");
        });

        modelBuilder.Entity<SpaceSubscription>(entity =>
        {
            entity.Property(e => e.CardExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.CardHolderName).HasMaxLength(450);
            entity.Property(e => e.CardNumber).HasMaxLength(450);
            entity.Property(e => e.CountryId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SecurityCode).HasMaxLength(450);
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Companies");
        });

        modelBuilder.Entity<UserCompany>(entity =>
        {
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.UserCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompanies_Companies");

            entity.HasOne(d => d.User).WithMany(p => p.UserCompanies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompanies_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.CompanyId).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.SpaceId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Companies");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
