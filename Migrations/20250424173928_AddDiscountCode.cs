using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.discount_type", "percentage,flat");

            migrationBuilder.AddColumn<int>(
                name: "discount_code_id",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "discount_codes",
                columns: table => new
                {
                    DiscountCodeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DiscountType = table.Column<int>(type: "discount_type", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaxUsage = table.Column<int>(type: "integer", nullable: true),
                    TimesUsed = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("discount_codes_pkey", x => x.DiscountCodeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_discount_code_id",
                table: "orders",
                column: "discount_code_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_discount_codes_discount_code_id",
                table: "orders",
                column: "discount_code_id",
                principalTable: "discount_codes",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_discount_codes_discount_code_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "discount_codes");

            migrationBuilder.DropIndex(
                name: "IX_orders_discount_code_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "discount_code_id",
                table: "orders");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:public.discount_type", "percentage,flat");
        }
    }
}
