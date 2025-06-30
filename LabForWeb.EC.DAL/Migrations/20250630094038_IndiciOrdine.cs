using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabForWeb.EC.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IndiciOrdine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ordini_Data",
                table: "Ordini",
                column: "Data");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_Numero_Anno",
                table: "Ordini",
                columns: new[] { "Numero", "Anno" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_Stato",
                table: "Ordini",
                column: "Stato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ordini_Data",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_Numero_Anno",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_Stato",
                table: "Ordini");
        }
    }
}
