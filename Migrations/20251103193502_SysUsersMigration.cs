using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryV2.Migrations
{
    /// <inheritdoc />
    public partial class SysUsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupervisedById",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SuperviserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SupervisedById",
                table: "AspNetUsers",
                column: "SupervisedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisedById",
                table: "AspNetUsers",
                column: "SupervisedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SupervisedById",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SupervisedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SupervisedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperviserId",
                table: "AspNetUsers");
        }
    }
}
