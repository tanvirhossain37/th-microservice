using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TH.RnD.API.TreeModels;

public partial class TreeDbContext : DbContext
{
    public TreeDbContext()
    {
    }

    public TreeDbContext(DbContextOptions<TreeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fruit> Fruits { get; set; }

    public virtual DbSet<Garden> Gardens { get; set; }

    public virtual DbSet<Tree> Trees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TreeDb;User ID=sa;Password=Adm!n123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fruit>(entity =>
        {
            entity.Property(e => e.Age).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.TreeId).HasMaxLength(450);

            entity.HasOne(d => d.Tree).WithMany(p => p.Fruits)
                .HasForeignKey(d => d.TreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fruits_Trees");
        });

        modelBuilder.Entity<Garden>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.Owner).HasMaxLength(256);
        });

        modelBuilder.Entity<Tree>(entity =>
        {
            entity.Property(e => e.Age).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GardenId).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);

            entity.HasOne(d => d.Garden).WithMany(p => p.Trees)
                .HasForeignKey(d => d.GardenId)
                .HasConstraintName("FK_Trees_Gardens");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
