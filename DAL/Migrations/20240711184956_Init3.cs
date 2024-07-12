using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Screens_FK_Sala",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "FK_Sala",
                table: "Schedules",
                newName: "FK_Screen");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_FK_Sala",
                table: "Schedules",
                newName: "IX_Schedules_FK_Screen");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Screens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Screens_FK_Screen",
                table: "Schedules",
                column: "FK_Screen",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Screens_FK_Screen",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Screens");

            migrationBuilder.RenameColumn(
                name: "FK_Screen",
                table: "Schedules",
                newName: "FK_Sala");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_FK_Screen",
                table: "Schedules",
                newName: "IX_Schedules_FK_Sala");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Screens_FK_Sala",
                table: "Schedules",
                column: "FK_Sala",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
