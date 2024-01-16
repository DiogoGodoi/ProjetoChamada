using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthTeste.Migrations.MeuContextoChamadaMigrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelaAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contato",
                table: "Escola",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contato",
                table: "Escola");
        }
    }
}
