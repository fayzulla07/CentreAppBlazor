using System;
using CentreAppBlazor.Server.Controllers;
using CentreAppBlazor.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CentreAppBlazor.Server.Data
{
    public partial class TechContext : DbContext
    {
        public TechContext()
        {
        }

        public TechContext(DbContextOptions<TechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvCurrentCosts> AvCurrentCosts { get; set; }
        public virtual DbSet<AvProfit> AvProfit { get; set; }
        public virtual DbSet<CustomerTypes> CustomerTypes { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DbLogs> DbLogs { get; set; }
        public virtual DbSet<HistorySaleView> HistorySaleView { get; set; }
        public virtual DbSet<LastOrderView> LastOrderView { get; set; }
        public virtual DbSet<MyCompany> MyCompany { get; set; }
        public virtual DbSet<ProductIncoms> ProductIncoms { get; set; }
        public virtual DbSet<ProductReturns> ProductReturns { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ReturnTotalByDayView> ReturnTotalByDayView { get; set; }
        public virtual DbSet<ReturnView> ReturnView { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SalesTotalByDayView> SalesTotalByDayView { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvCurrentCosts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AvCurrentCosts");
            });

            modelBuilder.Entity<AvProfit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AvProfit");
            });

            modelBuilder.Entity<CustomerTypes>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(350);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customers_CustomerTypes");
            });

            modelBuilder.Entity<DbLogs>(entity =>
            {
                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.RegDt).HasColumnType("datetime");
            });

            modelBuilder.Entity<HistorySaleView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HistorySaleView");

                entity.Property(e => e.AmountUnit).HasMaxLength(211);

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OptCost)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.RegDt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<LastOrderView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LastOrderView");
            });

            modelBuilder.Entity<MyCompany>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ProductIncoms>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.Kurs).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductionDt)
                    .HasColumnType("datetime")
                    .HasComment("Год выпуска");

                entity.Property(e => e.RegDt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductIncoms)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductIncoms_Products");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductIncoms)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProductIncoms_Suppliers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductIncoms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProductIncoms_Users");
            });

            modelBuilder.Entity<ProductReturns>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.Kurs).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegDt).HasColumnType("datetime");

                entity.HasOne(d => d.ProductSale)
                    .WithMany(p => p.ProductReturns)
                    .HasForeignKey(d => d.ProductSaleId)
                    .HasConstraintName("FK_ProductReturns_ProductSales");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductReturns)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProductReturns_Users");
            });

            modelBuilder.Entity<ProductSales>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.IsOptCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Kurs).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegDt).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProductSales_Customers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductSales_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProductSales_Users");
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(350);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UQ_ProductCode")
                    .IsUnique();

                entity.Property(e => e.Code).HasMaxLength(60);

                entity.Property(e => e.Description).HasMaxLength(350);

                entity.Property(e => e.Limit).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Products_ProductTypes");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Products_Units");
            });

            modelBuilder.Entity<ReturnTotalByDayView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReturnTotalByDayView");

                entity.Property(e => e.RegDt).HasColumnType("date");
            });

            modelBuilder.Entity<ReturnView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReturnView");

                entity.Property(e => e.AmountUnit).HasMaxLength(226);

                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.RegDt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<SalesTotalByDayView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesTotalByDayView");

                entity.Property(e => e.RegDt).HasColumnType("date");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(120);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("FIO");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
