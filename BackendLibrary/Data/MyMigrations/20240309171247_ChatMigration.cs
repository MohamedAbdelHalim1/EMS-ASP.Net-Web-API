using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

namespace BackendLibrary.Data.MyMigrations
{
    /// <inheritdoc />
    public partial class ChatMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Branches_BranchId",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "GeneralDepartmentId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "ApplicationUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GeneralDepartmentId",
                table: "Departments",
                column: "GeneralDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Branches_BranchId",
                table: "ApplicationUsers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments",
                column: "GeneralDepartmentId",
                principalTable: "GeneralDepartments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Branches_BranchId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_Departments_GeneralDepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "GeneralDepartmentId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "ApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Branches_BranchId",
                table: "ApplicationUsers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
