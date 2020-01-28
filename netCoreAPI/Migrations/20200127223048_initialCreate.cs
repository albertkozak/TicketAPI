using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreAPI.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "CreatedOn", "Description", "IsComplete", "Priority" },
                values: new object[] { 1, new DateTime(2020, 1, 27, 14, 30, 48, 109, DateTimeKind.Local).AddTicks(6554), "Clean house", false, 1 });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "CreatedOn", "Description", "IsComplete", "Priority" },
                values: new object[] { 2, new DateTime(2020, 1, 27, 14, 30, 48, 113, DateTimeKind.Local).AddTicks(568), "Bake cake", false, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");
        }
    }
}
