using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnLoginProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                schema: "Auth",
                table: "UserCredentials",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserCredentials_Login",
                schema: "Auth",
                table: "UserCredentials",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserCredentials_Login",
                schema: "Auth",
                table: "UserCredentials");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                schema: "Auth",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
