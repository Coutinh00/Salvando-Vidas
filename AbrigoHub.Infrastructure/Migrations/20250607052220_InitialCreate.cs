using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbrigoHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abrigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Latitude = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    Capacidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OcupacaoAtual = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, defaultValue: "aberto"),
                    CriadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abrigos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbrigosAvaliacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AbrigoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Avaliacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Comentario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbrigosAvaliacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbrigosAvaliacoes_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbrigosAvaliacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbrigosNecessidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AbrigoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TipoRecurso = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QuantidadeNecessaria = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NivelUrgencia = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "normal"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Atendida = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbrigosNecessidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbrigosNecessidades_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbrigosRecursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AbrigoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TipoRecurso = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QuantidadeDisponivel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbrigosRecursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbrigosRecursos_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AbrigoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomeDoador = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TipoRecurso = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecebidoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doacoes_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abrigos_UsuarioId",
                table: "Abrigos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AbrigosAvaliacoes_AbrigoId",
                table: "AbrigosAvaliacoes",
                column: "AbrigoId");

            migrationBuilder.CreateIndex(
                name: "IX_AbrigosAvaliacoes_UsuarioId",
                table: "AbrigosAvaliacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AbrigosNecessidades_AbrigoId",
                table: "AbrigosNecessidades",
                column: "AbrigoId");

            migrationBuilder.CreateIndex(
                name: "IX_AbrigosRecursos_AbrigoId",
                table: "AbrigosRecursos",
                column: "AbrigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_AbrigoId",
                table: "Doacoes",
                column: "AbrigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbrigosAvaliacoes");

            migrationBuilder.DropTable(
                name: "AbrigosNecessidades");

            migrationBuilder.DropTable(
                name: "AbrigosRecursos");

            migrationBuilder.DropTable(
                name: "Doacoes");

            migrationBuilder.DropTable(
                name: "Abrigos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
