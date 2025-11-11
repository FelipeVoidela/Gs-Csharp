using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GS_Csharp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALUNOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<string>(type: "NVARCHAR2(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUNOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROFESSORES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFESSORES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "COMUNIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ProfessorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMUNIDADES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMUNIDADES_PROFESSORES_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "PROFESSORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CURSOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ComunidadeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURSOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CURSOS_COMUNIDADES_ComunidadeId",
                        column: x => x.ComunidadeId,
                        principalTable: "COMUNIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSCRICOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AlunoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CursoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataInscricao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSCRICOES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSCRICOES_ALUNOS_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "ALUNOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INSCRICOES_CURSOS_CursoId",
                        column: x => x.CursoId,
                        principalTable: "CURSOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COMUNIDADES_ProfessorId",
                table: "COMUNIDADES",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CURSOS_ComunidadeId",
                table: "CURSOS",
                column: "ComunidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_INSCRICOES_AlunoId",
                table: "INSCRICOES",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_INSCRICOES_CursoId",
                table: "INSCRICOES",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INSCRICOES");

            migrationBuilder.DropTable(
                name: "ALUNOS");

            migrationBuilder.DropTable(
                name: "CURSOS");

            migrationBuilder.DropTable(
                name: "COMUNIDADES");

            migrationBuilder.DropTable(
                name: "PROFESSORES");
        }
    }
}
