﻿// <auto-generated />
using System;
using CentreAppBlazor.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CentreAppBlazor.Server.Migrations
{
    [DbContext(typeof(TechContext))]
    [Migration("20220823173847_Add_CustTypeDiscount")]
    partial class Add_CustTypeDiscount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.AvCurrentCosts", b =>
                {
                    b.Property<double?>("IncomeCost")
                        .HasColumnType("float");

                    b.Property<double?>("OptCost")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double?>("SaleCost")
                        .HasColumnType("float");

                    b.ToView("AvCurrentCosts");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.AvProfit", b =>
                {
                    b.Property<double>("TotalIncome")
                        .HasColumnType("float");

                    b.Property<double>("TotalIncomeTG")
                        .HasColumnType("float");

                    b.Property<double>("TotalOpt")
                        .HasColumnType("float");

                    b.Property<double>("TotalOptTG")
                        .HasColumnType("float");

                    b.Property<double>("TotalSale")
                        .HasColumnType("float");

                    b.Property<double>("TotalSaleTG")
                        .HasColumnType("float");

                    b.ToView("AvProfit");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.CustomerTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("CustomerTypes");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("CustomerTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double?>("GeoAltitude")
                        .HasColumnType("float");

                    b.Property<double?>("GeoLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("GeoLongitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.DbLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegDt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("DbLogs");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.HistorySaleView", b =>
                {
                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("AmountUnit")
                        .HasMaxLength(211)
                        .HasColumnType("nvarchar(211)");

                    b.Property<string>("Client")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<decimal>("IncomeCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("IsBank")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("OptCost")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.Property<int?>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegDt")
                        .HasColumnType("datetime");

                    b.Property<decimal>("SaleCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ToView("HistorySaleView");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.LastOrderView", b =>
                {
                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.ToView("LastOrderView");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.MyCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyCompany");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductIncoms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("IncomeCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("IncomeNumber")
                        .HasColumnType("int");

                    b.Property<double>("Kurs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal>("OptCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ProductionDt")
                        .HasColumnType("datetime")
                        .HasComment("Год выпуска");

                    b.Property<DateTime>("RegDt")
                        .HasColumnType("datetime");

                    b.Property<decimal>("SaleCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductIncoms");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductReturns", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<double>("Kurs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("ProductSaleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegDt")
                        .HasColumnType("datetime");

                    b.Property<decimal>("ReturnCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductSaleId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductReturns");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductSales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("IncomeCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("IsBank")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsOptCost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<double>("Kurs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("((1))");

                    b.Property<int?>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegDt")
                        .HasColumnType("datetime");

                    b.Property<decimal>("SaleCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductSales");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Description")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int>("Limit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<double>("RemainCount")
                        .HasColumnType("float");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("UQ_ProductCode")
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Reports", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ReturnTotalByDayView", b =>
                {
                    b.Property<DateTime?>("RegDt")
                        .HasColumnType("date");

                    b.Property<double>("ReturnCost")
                        .HasColumnType("float");

                    b.ToView("ReturnTotalByDayView");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ReturnView", b =>
                {
                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("AmountUnit")
                        .HasMaxLength(226)
                        .HasColumnType("nvarchar(226)");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegDt")
                        .HasColumnType("datetime");

                    b.Property<decimal>("ReturnCost")
                        .HasColumnType("decimal(18,2)");

                    b.ToView("ReturnView");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.SalesTotalByDayView", b =>
                {
                    b.Property<double?>("IncomeCost")
                        .HasColumnType("float");

                    b.Property<DateTime?>("RegDt")
                        .HasColumnType("date");

                    b.Property<double?>("SaleTotal")
                        .HasColumnType("float");

                    b.ToView("SalesTotalByDayView");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Suppliers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<double?>("HisDebt")
                        .HasColumnType("float");

                    b.Property<double?>("MyDebt")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Units", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("FIO");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Customers", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.CustomerTypes", "CustomerType")
                        .WithMany("Customers")
                        .HasForeignKey("CustomerTypeId")
                        .HasConstraintName("FK_Customers_CustomerTypes")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CustomerType");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductIncoms", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.Products", "Product")
                        .WithMany("ProductIncoms")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductIncoms_Products")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentreAppBlazor.Shared.Domain.Suppliers", "Supplier")
                        .WithMany("ProductIncoms")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK_ProductIncoms_Suppliers")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CentreAppBlazor.Shared.Domain.Users", "User")
                        .WithMany("ProductIncoms")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProductIncoms_Users")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Product");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductReturns", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.ProductSales", "ProductSale")
                        .WithMany("ProductReturns")
                        .HasForeignKey("ProductSaleId")
                        .HasConstraintName("FK_ProductReturns_ProductSales")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentreAppBlazor.Shared.Domain.Users", "User")
                        .WithMany("ProductReturns")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProductReturns_Users")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ProductSale");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductSales", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.Customers", "Customer")
                        .WithMany("ProductSales")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_ProductSales_Customers")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CentreAppBlazor.Shared.Domain.Products", "Product")
                        .WithMany("ProductSales")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductSales_Products")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentreAppBlazor.Shared.Domain.Users", "User")
                        .WithMany("ProductSales")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProductSales_Users")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Customer");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Products", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.ProductTypes", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .HasConstraintName("FK_Products_ProductTypes")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CentreAppBlazor.Shared.Domain.Units", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .HasConstraintName("FK_Products_Units")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ProductType");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Users", b =>
                {
                    b.HasOne("CentreAppBlazor.Shared.Domain.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Users_Roles");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.CustomerTypes", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Customers", b =>
                {
                    b.Navigation("ProductSales");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductSales", b =>
                {
                    b.Navigation("ProductReturns");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.ProductTypes", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Products", b =>
                {
                    b.Navigation("ProductIncoms");

                    b.Navigation("ProductSales");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Roles", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Suppliers", b =>
                {
                    b.Navigation("ProductIncoms");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Units", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CentreAppBlazor.Shared.Domain.Users", b =>
                {
                    b.Navigation("ProductIncoms");

                    b.Navigation("ProductReturns");

                    b.Navigation("ProductSales");
                });
#pragma warning restore 612, 618
        }
    }
}
