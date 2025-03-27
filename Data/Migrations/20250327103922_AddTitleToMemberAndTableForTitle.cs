using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToMemberAndTableForTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TitleId",
                table: "AspNetUsers",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Title_TitleId",
                table: "AspNetUsers",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Title_TitleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TitleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "AspNetUsers");
        }
    }
}
