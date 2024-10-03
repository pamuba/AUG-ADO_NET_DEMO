using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mantToManyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_AuthorFluent_Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_Author",
                table: "Fluent_Author");

            migrationBuilder.RenameTable(
                name: "Fluent_Author",
                newName: "Fluent_Authors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_Authors",
                table: "Fluent_Authors",
                column: "Author_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_Authors",
                table: "Fluent_Authors");

            migrationBuilder.RenameTable(
                name: "Fluent_Authors",
                newName: "Fluent_Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_Author",
                table: "Fluent_Author",
                column: "Author_Id");

            migrationBuilder.CreateTable(
                name: "Fluent_AuthorFluent_Book",
                columns: table => new
                {
                    Fluent_AuthorsAuthor_Id = table.Column<int>(type: "int", nullable: false),
                    Fluent_BooksBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_AuthorFluent_Book", x => new { x.Fluent_AuthorsAuthor_Id, x.Fluent_BooksBookID });
                    table.ForeignKey(
                        name: "FK_Fluent_AuthorFluent_Book_Fluent_Author_Fluent_AuthorsAuthor_Id",
                        column: x => x.Fluent_AuthorsAuthor_Id,
                        principalTable: "Fluent_Author",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fluent_AuthorFluent_Book_Fluent_Books_Fluent_BooksBookID",
                        column: x => x.Fluent_BooksBookID,
                        principalTable: "Fluent_Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_AuthorFluent_Book_Fluent_BooksBookID",
                table: "Fluent_AuthorFluent_Book",
                column: "Fluent_BooksBookID");
        }
    }
}
