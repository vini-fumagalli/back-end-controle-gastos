using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicontrolegastos.Migrations
{
    /// <inheritdoc />
    public partial class TesteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logado = table.Column<bool>(type: "bit", nullable: false),
                    Salario = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.Usuario);
                });

            migrationBuilder.CreateTable(
                name: "GASTO",
                columns: table => new
                {
                    Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: true),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    DataMax = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GASTO", x => new { x.Usuario, x.Tipo });
                    table.ForeignKey(
                        name: "FK_GASTO_USUARIO_Usuario",
                        column: x => x.Usuario,
                        principalTable: "USUARIO",
                        principalColumn: "Usuario",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GASTO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
