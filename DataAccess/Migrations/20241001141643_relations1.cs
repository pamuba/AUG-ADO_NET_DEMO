using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class relations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "BookDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_BookID",
                table: "BookDetails",
                column: "BookID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Books_BookID",
                table: "BookDetails",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Books_BookID",
                table: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookDetails_BookID",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "BookDetails");
        }
    }
}
