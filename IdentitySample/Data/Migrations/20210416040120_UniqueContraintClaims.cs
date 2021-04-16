using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentitySample.Data.Migrations
{
    public partial class UniqueContraintClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimType_ClaimValue_ControllerName_ActionName",
                table: "Claims",
                columns: new[] { "ClaimType", "ClaimValue", "ControllerName", "ActionName" },
                unique: true,
                filter: "[ClaimType] IS NOT NULL AND [ClaimValue] IS NOT NULL AND [ControllerName] IS NOT NULL AND [ActionName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Claims_ClaimType_ClaimValue_ControllerName_ActionName",
                table: "Claims");

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
