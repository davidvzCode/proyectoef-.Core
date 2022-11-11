using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoef.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreaccion",
                table: "Tarea",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("e01e4fdb-f1b4-464c-9f88-730813f3ef02"), null, "Actividades Personales", 50 },
                    { new Guid("e01e4fdb-f1b4-464c-9f88-730813f3efea"), null, "Actividades Pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreaccion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("e01e4fdb-f1b4-464c-9f88-730813f3ef10"), new Guid("e01e4fdb-f1b4-464c-9f88-730813f3efea"), null, new DateTime(2022, 9, 20, 16, 11, 39, 472, DateTimeKind.Local).AddTicks(7725), 1, "Pago de servicios publicos" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("e01e4fdb-f1b4-464c-9f88-730813f3ef02"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("e01e4fdb-f1b4-464c-9f88-730813f3ef10"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("e01e4fdb-f1b4-464c-9f88-730813f3efea"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreaccion",
                table: "Tarea",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
