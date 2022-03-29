using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivitySessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Session = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ActivitySessionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActivitySessions_ActivitySessionId",
                        column: x => x.ActivitySessionId,
                        principalTable: "ActivitySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Branch", "Color", "CreatedDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, "Apple", "Blue", new DateTime(2022, 3, 29, 2, 21, 50, 728, DateTimeKind.Local).AddTicks(4049), "Iphone13", 1200m },
                    { 2L, "Apple", "Green", new DateTime(2022, 3, 29, 2, 21, 50, 731, DateTimeKind.Local).AddTicks(1793), "Iphone13", 1200m },
                    { 3L, "Apple", "Gray", new DateTime(2022, 3, 29, 2, 21, 50, 731, DateTimeKind.Local).AddTicks(1883), "Iphone13", 1200m },
                    { 4L, "Samsung", "White", new DateTime(2022, 3, 29, 2, 21, 50, 731, DateTimeKind.Local).AddTicks(1899), "Galaxy Z Fold3", 900m },
                    { 5L, "Samsung", "Gray", new DateTime(2022, 3, 29, 2, 21, 50, 731, DateTimeKind.Local).AddTicks(1914), "Galaxy Z Fold3", 900m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivitySessionId",
                table: "Activities",
                column: "ActivitySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySessions_Session",
                table: "ActivitySessions",
                column: "Session");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ActivitySessions");
        }
    }
}
