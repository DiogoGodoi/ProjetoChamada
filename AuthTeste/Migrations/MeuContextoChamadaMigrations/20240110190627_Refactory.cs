using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthTeste.Migrations.MeuContextoChamadaMigrations
{
    /// <inheritdoc />
    public partial class Refactory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Cref = table.Column<int>(type: "int", nullable: false),
                    Contato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fk_Escola_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Escola_Fk_Escola_Id",
                        column: x => x.Fk_Escola_Id,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escola_Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_Professor_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Escola_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola_Professor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escola_Professor_Escola_Fk_Escola_Id",
                        column: x => x.Fk_Escola_Id,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escola_Professor_Professor_Fk_Professor_Id",
                        column: x => x.Fk_Professor_Id,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MdlProfessorMdlTurma",
                columns: table => new
                {
                    ProfessoresId = table.Column<int>(type: "int", nullable: false),
                    TurmasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MdlProfessorMdlTurma", x => new { x.ProfessoresId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_MdlProfessorMdlTurma_Professor_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MdlProfessorMdlTurma_Turma_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor_Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_Professor_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Turma_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professor_Turma_Professor_Fk_Professor_Id",
                        column: x => x.Fk_Professor_Id,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Professor_Turma_Turma_Fk_Turma_Id",
                        column: x => x.Fk_Turma_Id,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Escola_Cnpj",
                table: "Escola",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escola_Nome",
                table: "Escola",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escola_UrlImage",
                table: "Escola",
                column: "UrlImage",
                unique: true,
                filter: "[UrlImage] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_Professor_Fk_Escola_Id",
                table: "Escola_Professor",
                column: "Fk_Escola_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_Professor_Fk_Professor_Id",
                table: "Escola_Professor",
                column: "Fk_Professor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MdlProfessorMdlTurma_TurmasId",
                table: "MdlProfessorMdlTurma",
                column: "TurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Cpf",
                table: "Professor",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Cref",
                table: "Professor",
                column: "Cref",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Turma_Fk_Professor_Id",
                table: "Professor_Turma",
                column: "Fk_Professor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Turma_Fk_Turma_Id",
                table: "Professor_Turma",
                column: "Fk_Turma_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_Fk_Escola_Id",
                table: "Turma",
                column: "Fk_Escola_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escola_Professor");

            migrationBuilder.DropTable(
                name: "MdlProfessorMdlTurma");

            migrationBuilder.DropTable(
                name: "Professor_Turma");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Escola");
        }
    }
}
