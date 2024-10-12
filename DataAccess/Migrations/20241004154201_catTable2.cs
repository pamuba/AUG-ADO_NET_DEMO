using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class catTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into categories values('cat1')");
            migrationBuilder.Sql("insert into categories values('cat2')");
            migrationBuilder.Sql("insert into categories values('cat3')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
