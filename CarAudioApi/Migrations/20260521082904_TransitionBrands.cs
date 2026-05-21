using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarAudioApi.Migrations
{
    /// <inheritdoc />
    public partial class TransitionBrands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // ШАГ 1: Создаем новую таблицу для брендов
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });


            // ШАГ 2: Добавляем колонку brand_id в audio_components.
            // ВАЖНО: Делаем её временно NULLABLE (nullable: true), чтобы PostgreSQL не ругался на существующие строки.
            migrationBuilder.AddColumn<int>(
                name: "brand_id",
                table: "audio_components",
                type: "integer",
                nullable: true);

            // ШАГ 3: DATA MIGRATION (Чистый SQL)
            // 3.1. Копируем уникальные названия (например, Morel, Ural и т.д.) из старой колонки в новую таблицу брендов.
            migrationBuilder.Sql(
                @"INSERT INTO brands (name, description)
                SELECT DISTINCT brand, ''
                FROM audio_components
                WHERE brand IS NOT NULL AND brand !='';"
            );
            // 3.2. Обновляем audio_components: проставляем правильный brand_id, сопоставляя строки.
            migrationBuilder.Sql(
                @"UPDATE audio_components ac
                SET brand_id = b.id
                FROM brands b
                WHERE ac.brand = b.name;"
            );

            // ШАГ 4: Затягиваем гайки. 
            // Теперь, когда у всех динамиков есть brand_id, делаем колонку обязательной (NOT NULL)
            migrationBuilder.AlterColumn<int>(
                name: "brand_id",
                table: "audio_components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            // ШАГ 5: Добавляем внешний ключ (Foreign Key)
            migrationBuilder.AddForeignKey(
                name: "fk_audio_components_brands_brand_id",
                table: "audio_components",
                column: "brand_id",
                principalTable: "brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "ix_audio_components_brand_id",
                table: "audio_components",
                column: "brand_id");

            // ШАГ 6: Только теперь, когда данные в безопасности, удаляем старую текстовую колонку
            migrationBuilder.DropColumn(
                name: "brand",
                table: "audio_components");


        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_audio_components_brands_brand_id",
                table: "audio_components");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropIndex(
                name: "ix_audio_components_brand_id",
                table: "audio_components");

            migrationBuilder.DropColumn(
                name: "brand_id",
                table: "audio_components");
        }
    }
}
