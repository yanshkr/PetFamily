using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "breed_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets",
                column: "specie_id",
                principalTable: "species",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "breed_id",
                table: "pets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets",
                column: "specie_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
