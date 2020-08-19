using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentreAppBlazor.Server.Migrations
{
    public partial class TechMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: false),
                    RegDt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyCompany",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 120, nullable: true),
                    HisDebt = table.Column<double>(nullable: true),
                    MyDebt = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    GeoLatitude = table.Column<double>(nullable: true),
                    GeoLongitude = table.Column<double>(nullable: true),
                    GeoAltitude = table.Column<double>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false, comment: "FIO"),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    LoginName = table.Column<string>(maxLength: 150, nullable: false),
                    Password = table.Column<string>(maxLength: 350, nullable: false),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Code = table.Column<string>(maxLength: 60, nullable: true),
                    Description = table.Column<string>(maxLength: 350, nullable: true),
                    ProductTypeId = table.Column<int>(nullable: true),
                    RemainCount = table.Column<double>(nullable: false),
                    UnitId = table.Column<int>(nullable: true),
                    Limit = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_Units",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductIncoms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    IncomeCost = table.Column<double>(nullable: false),
                    SaleCost = table.Column<double>(nullable: false),
                    OptCost = table.Column<double>(nullable: false),
                    ProductionDt = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Год выпуска"),
                    SupplierId = table.Column<int>(nullable: true),
                    RegDt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(maxLength: 300, nullable: true),
                    Kurs = table.Column<double>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIncoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIncoms_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIncoms_Suppliers",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductIncoms_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    SaleCost = table.Column<double>(nullable: false),
                    IsOptCost = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    CustomerId = table.Column<int>(nullable: true),
                    RegDt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(maxLength: 300, nullable: true),
                    IncomeCost = table.Column<double>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: true),
                    IsBank = table.Column<bool>(nullable: true),
                    Kurs = table.Column<double>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSales_Customers",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductSales_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSales_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductReturns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSaleId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ReturnCost = table.Column<double>(nullable: false),
                    RegDt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(maxLength: 300, nullable: true),
                    Kurs = table.Column<double>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReturns_ProductSales",
                        column: x => x.ProductSaleId,
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReturns_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIncoms_ProductId",
                table: "ProductIncoms",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIncoms_SupplierId",
                table: "ProductIncoms",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIncoms_UserId",
                table: "ProductIncoms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturns_ProductSaleId",
                table: "ProductReturns",
                column: "ProductSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturns_UserId",
                table: "ProductReturns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductCode",
                table: "Products",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_CustomerId",
                table: "ProductSales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductId",
                table: "ProductSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_UserId",
                table: "ProductSales",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbLogs");

            migrationBuilder.DropTable(
                name: "MyCompany");

            migrationBuilder.DropTable(
                name: "ProductIncoms");

            migrationBuilder.DropTable(
                name: "ProductReturns");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
