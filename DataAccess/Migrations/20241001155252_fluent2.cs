using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fluent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "BookDetail_Fluent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Book_Fluent",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Fluent", x => x.BookID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDetail_Fluent_BookID",
                table: "BookDetail_Fluent",
                column: "BookID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetail_Fluent_Book_Fluent_BookID",
                table: "BookDetail_Fluent",
                column: "BookID",
                principalTable: "Book_Fluent",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetail_Fluent_Book_Fluent_BookID",
                table: "BookDetail_Fluent");

            migrationBuilder.DropTable(
                name: "Book_Fluent");

            migrationBuilder.DropIndex(
                name: "IX_BookDetail_Fluent_BookID",
                table: "BookDetail_Fluent");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "BookDetail_Fluent");
        }
    }
}
