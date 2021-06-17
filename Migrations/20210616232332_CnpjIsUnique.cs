using Microsoft.EntityFrameworkCore.Migrations;

namespace hondaerp.Migrations
{
    public partial class CnpjIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Suplier",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Suplier_CNPJ",
                table: "Suplier",
                column: "CNPJ",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suplier_CNPJ",
                table: "Suplier");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Suplier",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
