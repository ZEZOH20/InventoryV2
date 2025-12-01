using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryV2.Migrations
{
    /// <inheritdoc />
    public partial class AllInventoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperviserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SupervisedById",
                table: "AspNetUsers",
                newName: "SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SupervisedById",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SupervisorId");

            migrationBuilder.AddColumn<int>(
                name: "WorkingWarehouseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditableEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditableEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Release_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    War_Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Release_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Release_Orders_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Release_Orders_Warehouses_War_Number",
                        column: x => x.War_Number,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supply_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Supplier_ID = table.Column<int>(type: "int", nullable: false),
                    War_Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supply_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supply_Orders_Suppliers_Supplier_ID",
                        column: x => x.Supplier_ID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supply_Orders_Warehouses_War_Number",
                        column: x => x.War_Number,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Supplier_ID = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Orders_Suppliers_Supplier_ID",
                        column: x => x.Supplier_ID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfer_Orders_Warehouses_From",
                        column: x => x.From,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfer_Orders_Warehouses_To",
                        column: x => x.To,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    War_Id = table.Column<int>(type: "int", nullable: false),
                    Supplier_ID = table.Column<int>(type: "int", nullable: false),
                    MFD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_Amount = table.Column<double>(type: "float", nullable: false),
                    Total_Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Products_AuditableEntity_Id",
                        column: x => x.Id,
                        principalTable: "AuditableEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouse_Products_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouse_Products_Suppliers_Supplier_ID",
                        column: x => x.Supplier_ID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouse_Products_Warehouses_War_Id",
                        column: x => x.War_Id,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RO_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RO_Amount = table.Column<double>(type: "float", nullable: false),
                    RO_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RO_Price = table.Column<double>(type: "float", nullable: false),
                    RO_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Products_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RO_Products_Release_Orders_RO_Id",
                        column: x => x.RO_Id,
                        principalTable: "Release_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SO_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SO_Amount = table.Column<double>(type: "float", nullable: false),
                    SO_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SO_Price = table.Column<double>(type: "float", nullable: false),
                    SO_MFD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SO_EXP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SO_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SO_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SO_Products_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SO_Products_Supply_Orders_SO_Id",
                        column: x => x.SO_Id,
                        principalTable: "Supply_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TO_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TO_Amount = table.Column<double>(type: "float", nullable: false),
                    TO_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TO_Price = table.Column<double>(type: "float", nullable: false),
                    TO_MFD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TO_EXP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TO_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TO_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TO_Products_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TO_Products_Transfer_Orders_TO_Id",
                        column: x => x.TO_Id,
                        principalTable: "Transfer_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkingWarehouseId",
                table: "AspNetUsers",
                column: "WorkingWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Release_Orders_Customer_ID",
                table: "Release_Orders",
                column: "Customer_ID",
                unique: true,
                filter: "[Customer_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Release_Orders_Number",
                table: "Release_Orders",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Release_Orders_War_Number",
                table: "Release_Orders",
                column: "War_Number");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Products_Product_Id",
                table: "RO_Products",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Products_RO_Id",
                table: "RO_Products",
                column: "RO_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SO_Products_Product_Id",
                table: "SO_Products",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SO_Products_SO_Id",
                table: "SO_Products",
                column: "SO_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Supply_Orders_Number",
                table: "Supply_Orders",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Supply_Orders_Supplier_ID",
                table: "Supply_Orders",
                column: "Supplier_ID",
                unique: true,
                filter: "[Supplier_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Supply_Orders_War_Number",
                table: "Supply_Orders",
                column: "War_Number");

            migrationBuilder.CreateIndex(
                name: "IX_TO_Products_Product_Id",
                table: "TO_Products",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TO_Products_TO_Id",
                table: "TO_Products",
                column: "TO_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Orders_From",
                table: "Transfer_Orders",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Orders_Number",
                table: "Transfer_Orders",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Orders_Supplier_ID",
                table: "Transfer_Orders",
                column: "Supplier_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Orders_To",
                table: "Transfer_Orders",
                column: "To");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Products_Product_Id",
                table: "Warehouse_Products",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Products_Supplier_ID",
                table: "Warehouse_Products",
                column: "Supplier_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Products_War_Id",
                table: "Warehouse_Products",
                column: "War_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ManagerId",
                table: "Warehouses",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Number",
                table: "Warehouses",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_OwnerId",
                table: "Warehouses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisorId",
                table: "AspNetUsers",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Warehouses_WorkingWarehouseId",
                table: "AspNetUsers",
                column: "WorkingWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Warehouses_WorkingWarehouseId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RO_Products");

            migrationBuilder.DropTable(
                name: "SO_Products");

            migrationBuilder.DropTable(
                name: "TO_Products");

            migrationBuilder.DropTable(
                name: "Warehouse_Products");

            migrationBuilder.DropTable(
                name: "Release_Orders");

            migrationBuilder.DropTable(
                name: "Supply_Orders");

            migrationBuilder.DropTable(
                name: "Transfer_Orders");

            migrationBuilder.DropTable(
                name: "AuditableEntity");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkingWarehouseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkingWarehouseId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "AspNetUsers",
                newName: "SupervisedById");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SupervisorId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SupervisedById");

            migrationBuilder.AddColumn<Guid>(
                name: "SuperviserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisedById",
                table: "AspNetUsers",
                column: "SupervisedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
